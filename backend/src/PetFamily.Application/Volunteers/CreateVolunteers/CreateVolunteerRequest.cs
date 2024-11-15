using PetFamily.Domain.Volunteer;

namespace PetFamily.Application.Volunteers.CreateVolunteers;

public record CreateVolunteerRequest(
    string Descriptions, 
    string PhoneNumbers, 
    string ExperienceYears, 
    string Name, 
    string Surname, 
    string SecondName,
    IEnumerable<SocialNetworkDto> SocialNetworks,
    IEnumerable<AssistanceDetailDto> AssistanceDetails
    );


public record SocialNetworkDto(string Name, string Path);
public record AssistanceDetailDto(string Name, string Description);