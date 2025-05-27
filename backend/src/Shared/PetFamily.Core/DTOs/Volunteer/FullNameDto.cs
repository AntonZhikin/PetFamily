namespace PetFamily.Core.DTOs.Volunteer;

public record FullNameDto(
    string Name,
    string Surname,
    string? SecondName);