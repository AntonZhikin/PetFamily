using PetFamily.Application.Species.AddBreedToSpecies;

namespace PetFamily.API.Controllers.Species.Request;

public record AddBreedToSpeciesRequest(string Name)
{
    public AddBreedToSpeciesCommand ToCommand(Guid speciesId) =>
        new(speciesId, Name);


}
