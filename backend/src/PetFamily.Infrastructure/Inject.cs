using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.AspNetCore;
using PetFamily.Application.Database;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Volunteers;
using PetFamily.Infrastructure.Interceptors;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Repositories;
using MinioOptions = PetFamily.Infrastructure.Options.MinioOptions;

namespace PetFamily.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();
        //services.AddSingleton<SoftDeleteInterceptors>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddMinio(configuration);
        services.AddScoped<IVolunteerRepository, VolunteersRepository>();

        return services;
    }

    private static IServiceCollection AddMinio(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioOptions>(
            configuration.GetSection(MinioOptions.MINIO));
        
        services.AddMinio(options =>
        {
            MinioOptions minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>() 
                                        ?? throw new NullReferenceException("Minio options not found"); ;
            
            options.WithEndpoint(minioOptions.Endpoint);
            
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.WithSSL);
        });

        services.AddScoped<IFileProvider, MinioProvider>();

        return services;
    }
}