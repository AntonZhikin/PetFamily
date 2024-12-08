using PetFamily.Application.Species.AddBreedToSpecies;
using PetFamily.Domain.Speciess.SpeciesID;

namespace PetFamily.API.Controllers.Species.Request;

public record AddBreedToSpeciesRequest(string Name)
{
    public AddBreedToSpeciesCommand ToCommand(Guid speciesId) =>
        new(speciesId, Name);


}
