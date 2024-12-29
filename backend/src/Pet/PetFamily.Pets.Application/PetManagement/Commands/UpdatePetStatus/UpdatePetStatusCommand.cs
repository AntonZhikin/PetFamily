using PetFamily.Core.Abstractions;

namespace PetFamily.Pets.Application.PetManagement.Commands.UpdatePetStatus;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, string NewStatus) : ICommand;
