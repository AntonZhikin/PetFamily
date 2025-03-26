using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core;
using PetFamily.Species.Application;
using PetFamily.Species.Application.Species;
using PetFamily.Species.Contracts;
using PetFamily.Species.Infrastructure.BackgroundService;
using PetFamily.Species.Infrastructure.DbContext;
using PetFamily.Species.Infrastructure.Repository;
using PetFamily.Species.Presentation;

namespace PetFamily.Species.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRepositories()
            .AddDatabase()
            .AddDbContext(configuration)
            .AddHostedServices();
        
        services.AddScoped<ISpecieContract, SpecieContracts>();
            
        return services;
    }
    
    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddScoped<WriteDbContext>(_ => 
            new WriteDbContext(configuration.GetConnectionString("Database")!));
        services.AddScoped<IReadDbContext, ReadDbContext>(_ => 
            new ReadDbContext(configuration.GetConnectionString("Database")!));

        return services;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Specie);
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<ISpeciesRepository, SpeciesRepository>();

        return services;
    }
    
    private static IServiceCollection AddHostedServices(
        this IServiceCollection services)
    {
        services.AddHostedService<DeleteExpiredBreedBackgroundService>();
        return services;
    }

}