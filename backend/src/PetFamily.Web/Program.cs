using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Infrastructure;
using PetFamily.Accounts.Infrastructure.Seeding;
using PetFamily.Accounts.Presentation;
using PetFamily.Pets.Application;
using PetFamily.Pets.Controllers.Volunteers;
using PetFamily.Pets.Infrastructure;
using PetFamily.Species.Application;
using PetFamily.Species.Infrastructure;
using PetFamily.Species.Presentation.Species;
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

builder.Services
    .AddPetsApplication()
    .AddPetsInfrastructure(builder.Configuration)

    .AddSpeciesApplication()
    .AddSpeciesInfrastructure(builder.Configuration)

    .AddAccountsApplication()
    .AddAccountsPresentation()
    .AddAccountsInfrastructure(builder.Configuration);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(VolunteersController).Assembly)
    .AddApplicationPart(typeof(SpeciesController).Assembly)
    .AddApplicationPart(typeof(AccountsController).Assembly);

var app = builder.Build();

var accountsSeeder = app.Services.GetRequiredService<AccountsSeeder>();

await accountsSeeder.SeedAsync();

app.UseSerilogRequestLogging();

app.UseExceptionMiddleware();

// Configure the HTTP request pipeline.
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

app.MapGet("api/users", () =>
{
    return Results.BadRequest("Users not found");
    
    List<string> users = ["user1", "user2", "user3"];
    
    return Results.Ok(users);
});

app.Run();

namespace PetFamily.Web
{
    public partial class Program { }

    public class AuthOptions : AuthenticationSchemeOptions
    {
    
    }
}