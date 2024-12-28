using PetFamily.Pets.Application.PetManagement.Commands.MovePositionPet;

namespace PetFamily.Pets.Controllers.Volunteers.Request;

public record MovePositionPetRequest(int NewPosition)
{
    public MovePositionPetCommand ToCommand(Guid id, Guid petId) => new
    (
        id,
        petId,
        NewPosition
    );
}