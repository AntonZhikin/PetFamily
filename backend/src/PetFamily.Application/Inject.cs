using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstractions;
using PetFamily.Application.PetManagement.Commands.AddPet;
using PetFamily.Application.PetManagement.Commands.Create;
using PetFamily.Application.PetManagement.Commands.DeleteFilesToPet;
using PetFamily.Application.PetManagement.Commands.DeleteHard;
using PetFamily.Application.PetManagement.Commands.DeleteSoft;
using PetFamily.Application.PetManagement.Commands.FIles.AddPet;
using PetFamily.Application.PetManagement.Commands.FIles.DeletePet;
using PetFamily.Application.PetManagement.Commands.FIles.GetPet;
using PetFamily.Application.PetManagement.Commands.MovePositionPet;
using PetFamily.Application.PetManagement.Commands.UpdateAssistanceDetail;
using PetFamily.Application.PetManagement.Commands.UpdateMainInfo;
using PetFamily.Application.PetManagement.Commands.UpdateSocialNetworks;
using PetFamily.Application.PetManagement.Commands.UploadFilesToPet;
using PetFamily.Application.PetManagement.Queries.GetVolunteersWithPagination;

namespace PetFamily.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddCommands()
            .AddQueries()
            .AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
    
    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes
                .AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelf()
            .WithScopedLifetime());
    }
}