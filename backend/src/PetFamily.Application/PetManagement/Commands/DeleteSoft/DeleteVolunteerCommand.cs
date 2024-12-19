using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagement.Commands.DeleteSoft;

public record DeleteVolunteerCommand (Guid VolunteerId) : ICommand;