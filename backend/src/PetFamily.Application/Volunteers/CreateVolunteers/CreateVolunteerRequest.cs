using PetFamily.Application.Volunteers.DTOs;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Application.Volunteers.CreateVolunteers;

public record CreateVolunteerRequest(
    string Descriptions, 
    string PhoneNumbers, 
    string ExperienceYears, 
    string Name, 
    string Surname, 
    string SecondName,
    IReadOnlyList<SocialNetworkDto> SocialNetworks,
    IReadOnlyList<AssistanceDetailDto> AssistanceDetails
    );