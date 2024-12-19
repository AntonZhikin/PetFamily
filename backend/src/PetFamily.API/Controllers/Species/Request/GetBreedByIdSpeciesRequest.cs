using PetFamily.Application.Species.Queries.GetBreedByIdSpecies;

namespace PetFamily.API.Controllers.Species.Request;

public record GetBreedByIdSpeciesRequest(int Page, int PageSize)
{
    public GetBreedByIdSpeciesQuery ToQuery(Guid speciesId)
    {
        return new GetBreedByIdSpeciesQuery(speciesId, Page, PageSize);
    }
}
