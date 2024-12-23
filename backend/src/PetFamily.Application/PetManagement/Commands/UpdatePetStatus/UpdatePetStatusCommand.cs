using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagement.Commands.UpdatePetStatus;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, string NewStatus) : ICommand;
