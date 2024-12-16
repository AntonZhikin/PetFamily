using PetFamily.Domain.PetManagement.ValueObjects;

namespace PetFamily.Application.DTOs;

public class VolunteerDto
{
     public Guid Id { get; init; }
     
     public string Description { get; init; } = string.Empty;
     
     public string PhoneNumber { get; init; } = string.Empty;
     
     public string ExperienceYears { get; init; } = string.Empty;
     
     public string FullName { get; init; } = string.Empty;
     
     public PetDto[] Pets { get; init; } = [];
}