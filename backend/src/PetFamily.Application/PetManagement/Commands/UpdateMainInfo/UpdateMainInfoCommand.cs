using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagement.Commands.UpdateMainInfo;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    string? Descriptions,
    string? PhoneNumbers,
    string? ExperienceYears,
    string? Name,
    string? Surname,
    string? SecondName) : ICommand;
