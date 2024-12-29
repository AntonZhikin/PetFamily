using PetFamily.Core.Abstractions;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteSoft;

public record DeleteVolunteerCommand (Guid VolunteerId) : ICommand;