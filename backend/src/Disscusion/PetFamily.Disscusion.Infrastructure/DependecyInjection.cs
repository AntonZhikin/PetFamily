using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core;
using PetFamily.Disscusion.Application;
using PetFamily.Disscusion.Infrastructure.DbContext;

namespace PetFamily.Disscusion.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDiscussionsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddRepositories()
            .AddDbContext(configuration)
            .AddUnitOfWork();
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IDiscussionsRepository, DiscussionsRepository>();
        return services;
    }
    
    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddScoped<DiscussionsWriteDbContext>(_ => 
            new DiscussionsWriteDbContext(configuration.GetConnectionString("Database")!));
        
        services.AddScoped<IDiscussionsReadDbContext, DiscussionsReadDbContext>(_ =>
            new DiscussionsReadDbContext(configuration.GetConnectionString("Database")!));

        return services;
    }
    
    private static IServiceCollection AddUnitOfWork(
        this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Disscusion);
        return services;
    }
}