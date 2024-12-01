using Microsoft.AspNetCore.Diagnostics;
using PetFamily.API;
using PetFamily.API.Middlewares;
using PetFamily.Application;
using PetFamily.Infrastructure;
using Serilog;
using Serilog.Events;

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
    

    

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSerilog();

builder.Services
    .AddInfrastructure(builder.Configuration) 
    .AddApplication();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.ApplyMigration();
}
 
app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    await next.Invoke();
});

app.Run();