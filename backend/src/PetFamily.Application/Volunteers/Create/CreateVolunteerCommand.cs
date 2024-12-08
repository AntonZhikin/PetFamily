using PetFamily.Application.Volunteers.DTOs;

namespace PetFamily.Application.Volunteers.Create;

public record CreateVolunteerCommand(
    string Descriptions, 
    string PhoneNumbers, 
    string ExperienceYears, 
    string Name, 
    string Surname, 
    string SecondName,
    SocialNetworkListDto SocialNetworkList,
    AssistanceDetailListDto AssistanceDetailList
    );