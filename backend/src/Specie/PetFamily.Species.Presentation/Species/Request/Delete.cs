using PetFamily.Species.Application.Species.Commands.Delete;

namespace PetFamily.Species.Presentation.Species.Request;

public record Delete(Guid SpeciesId)
{
    public DeleteCommand ToCommand()
        => new(SpeciesId);
}