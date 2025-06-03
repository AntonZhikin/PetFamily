using FilesService.Communication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Infrastructure;
using PetFamily.Accounts.Infrastructure.DbContexts;
using PetFamily.Accounts.Infrastructure.Seeding;
using PetFamily.Accounts.Presentation;
using PetFamily.Disscusion.Application;
using PetFamily.Disscusion.Infrastructure;
using PetFamily.Disscusion.Presentation;
using PetFamily.Pets.Application;
using PetFamily.Pets.Controllers.Volunteers;
using PetFamily.Pets.Infrastructure;
using PetFamily.Species.Application;
using PetFamily.Species.Infrastructure;
using PetFamily.Species.Presentation.Species;
using PetFamily.VolunteerRequest.Application;
using PetFamily.VolunteerRequest.Infrastructure;
using PetFamily.VolunteerRequest.Presentation;
using PetFamily.Web.Middlewares;
using Serilog;
using Serilog.Events;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(o =>
{
    o.CombineLogs = true; 
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq") 
                 ?? throw new ArgumentNullException("Seq")) 
    .Enrich.WithThreadId()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName() 
    .Enrich.WithEnvironmentName()
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Information)
    .CreateLogger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
builder.Services.AddSerilog();

builder.Services.AddAuthorization();

builder.Services.AddFileHttpCommunication(builder.Configuration);

builder.Services
    .AddPetsApplication()
    .AddPetsInfrastructure(builder.Configuration)

    .AddSpeciesApplication()
    .AddSpeciesInfrastructure(builder.Configuration)

    .AddAccountsApplication()
    .AddAccountsPresentation()
    .AddAccountsInfrastructure(builder.Configuration)

    .AddVolunteerRequestApplication()
    .AddVolunteerRequestsInfrastructure(builder.Configuration)

    .AddDiscussionApplication()
    .AddDiscussionsInfrastructure(builder.Configuration)
    .AddDiscussionPresentation();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(VolunteersController).Assembly)
    .AddApplicationPart(typeof(SpeciesController).Assembly)
    .AddApplicationPart(typeof(AccountsController).Assembly)
    .AddApplicationPart(typeof(VolunteerRequestsController).Assembly)
    .AddApplicationPart(typeof(DiscussionsController).Assembly);

var app = builder.Build();


await using var scope = app.Services.CreateAsyncScope();

var dbContext = scope.ServiceProvider.GetRequiredService<WriteAccountsDbContext>();

await dbContext.Database.MigrateAsync();

var accountsSeeder = app.Services.GetRequiredService<AccountsSeeder>();

await accountsSeeder.SeedAsync();


app.UseSerilogRequestLogging();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //await app.ApplyMigration();
}

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseCors(config => 
{ 
    config.WithOrigins("http://localhost:5173")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials(); 
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    await next.Invoke();
});

app.Run();

namespace PetFamily.Web
{
    public partial class Program { }

    public class AuthOptions : AuthenticationSchemeOptions
    {
    
    }
}