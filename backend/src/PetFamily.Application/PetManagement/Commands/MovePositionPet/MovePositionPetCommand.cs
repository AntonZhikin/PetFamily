using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagement.Commands.MovePositionPet;

public record MovePositionPetCommand(Guid VolunteerId, Guid PetId, int NewPosition) : ICommand;