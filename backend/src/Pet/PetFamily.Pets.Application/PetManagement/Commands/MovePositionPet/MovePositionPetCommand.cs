using PetFamily.Core.Abstractions;

namespace PetFamily.Pets.Application.PetManagement.Commands.MovePositionPet;

public record MovePositionPetCommand(Guid VolunteerId, Guid PetId, int NewPosition) : ICommand;