using PetFamily.Application.PetManagement.Commands.MovePositionPet;
using PetFamily.Domain.PetManagement.Ids;

namespace PetFamily.API.Controllers.Volunteers.Request;

public record MovePositionPetRequest(int NewPosition)
{
    public MovePositionPetCommand ToCommand(Guid id, Guid petId) => new
    (
        id,
        petId,
        NewPosition
    );
}