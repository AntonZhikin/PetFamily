using PetFamily.Species.Application.Species.Commands.AddBreedToSpecies;

namespace PetFamily.Species.Presentation.Species.Request;

public record AddBreedToSpeciesRequest(string Name)
{
    public AddBreedToSpeciesCommand ToCommand(Guid speciesId) =>
        new(speciesId, Name);


}
