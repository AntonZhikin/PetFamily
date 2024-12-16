using PetFamily.Application.Abstractions;
using PetFamily.Application.DTOs;

namespace PetFamily.Application.PetManagement.Commands.Create;

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