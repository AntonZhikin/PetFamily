using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.DeleteVolunteer;
using PetFamily.Application.Volunteers.DeleteVolunteerHard;
using PetFamily.Application.Volunteers.FIles.DeletePet;
using PetFamily.Application.Volunteers.FIles.GetPetFiles;
using PetFamily.Application.Volunteers.UpdateAssistanceDetail;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateSocialNetworks;

namespace PetFamily.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateMainInfoHandler>();
        services.AddScoped<UpdateSocialNetworkHandler>();
        services.AddScoped<UpdateAssistanceDetailHandler>();
        services.AddScoped<DeleteVolunteerHandler>();
        services.AddScoped<DeleteVolunteerHardHandler>();
        services.AddScoped<AddPetFilesHandler>();
        services.AddScoped<GetPetFilesHandler>();
        services.AddScoped<DeletePetFilesHandler>();
        
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}