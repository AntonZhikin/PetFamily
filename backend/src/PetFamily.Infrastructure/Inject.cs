using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Volunteers;
using PetFamily.Infrastructure.Interceptors;
using PetFamily.Infrastructure.Repositories;

namespace PetFamily.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddSingleton<SoftDeleteInterceptors>();
        
        
        services.AddScoped<IVolunteerRepository, VolunteersRepository>();

        return services;
    }
}