using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;

namespace PetFamily.Disscusion.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddDiscussionApplication(this IServiceCollection services)
    {
        services
            .AddCommands()
            .AddQueries()
            .AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
    
    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(IQueryHandler<,>), typeof(IQueryValidationHandler<,>)))
            .AsSelf()
            .WithScopedLifetime());
    }
}