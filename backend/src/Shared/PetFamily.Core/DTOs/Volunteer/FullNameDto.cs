namespace PetFamily.Core.DTOs.Volunteer;

public record FullNameDto(
    string FirstName,
    string SecondName,
    string? LastName);