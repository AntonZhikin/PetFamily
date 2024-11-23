using PetFamily.Application.Volunteers.DTOs;

namespace PetFamily.Application.Volunteers.Create;

public record CreateVolunteerRequest(
    string Descriptions, 
    string PhoneNumbers, 
    string ExperienceYears, 
    string Name, 
    string Surname, 
    string SecondName,
    List<SocialNetworkDto> SocialNetworks,
    List<AssistanceDetailDto> AssistanceDetails
    );