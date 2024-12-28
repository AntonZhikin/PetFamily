using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs.ValueObject;

namespace PetFamily.Pets.Application.PetManagement.Commands.Create;

public record CreateVolunteerCommand(
    string Descriptions, 
    string PhoneNumbers, 
    string ExperienceYears, 
    string Name, 
    string Surname, 
    string SecondName,
    SocialNetworkListDto SocialNetworkList,
    AssistanceDetailListDto AssistanceDetailList
    ) : ICommand;