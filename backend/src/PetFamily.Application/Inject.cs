using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Voluunters.CreateVoluunters;

namespace PetFamily.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();

        return services;
    }
}