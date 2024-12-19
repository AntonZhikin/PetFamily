using PetFamily.Application.Species.Delete;

namespace PetFamily.API.Controllers.Species.Request;

public record Delete(Guid SpeciesId)
{
    public DeleteCommand ToCommand()
        => new(SpeciesId);
}