using PetFamily.Core.DTOs.Pets;

namespace PetFamily.Core.DTOs.Volunteer;

public class VolunteerDto
{
     public Guid Id { get; init; }
     
     // public string Description { get; init; } = string.Empty;
     //
     // public string Name { get; init; } = string.Empty;
     //
     // public string Surname { get; init; } = string.Empty;
     //
     // public string SecondName { get; init; } = string.Empty;
     //
     // public string PhoneNumber { get; init; } = string.Empty;
     //
     // public string ExperienceYears { get; init; } = string.Empty;
     
     public PetDto[] Pets { get; init; } = [];
}