using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.DeleteHard;
using PetFamily.Application.Volunteers.DeleteSoft;
using PetFamily.Application.Volunteers.FIles.AddPet;
using PetFamily.Application.Volunteers.FIles.DeletePet;
using PetFamily.Application.Volunteers.FIles.GetPet;
using PetFamily.Application.Volunteers.UpdateAssistanceDetail;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateSocialNetworks;
using PetFamily.Application.Volunteers.UploadFilesToPet;

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
        services.AddScoped<AddPetHandler>();
        services.AddScoped<UploadFileToPetHandler>();
        
        
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}