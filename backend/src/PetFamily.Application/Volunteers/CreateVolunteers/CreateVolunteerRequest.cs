using PetFamily.Domain.Volunteer;

namespace PetFamily.Application.Volunteers.CreateVolunteers;

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


public record SocialNetworkDto(string Name, string Path);
public record AssistanceDetailDto(string Name, string Description);