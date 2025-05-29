using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Disscusion.Application;
using PetFamily.Disscusion.Contracts;
using PetFamily.Disscusion.Infrastructure;

namespace PetFamily.Disscusion.Presentation;

public static class DependencyInjection 
{
    public static IServiceCollection AddDiscussionPresentation(this IServiceCollection services)
    {
        services.AddScoped<IDiscussionContract, DiscussionContract>();
        return services;
    }
}