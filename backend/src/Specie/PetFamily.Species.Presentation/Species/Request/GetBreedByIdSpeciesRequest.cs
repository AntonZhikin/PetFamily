using PetFamily.Species.Application.Species.Queries.GetBreedByIdSpecies;

namespace PetFamily.Species.Presentation.Species.Request;

public record GetBreedByIdSpeciesRequest(int Page, int PageSize)
{
    public GetBreedByIdSpeciesQuery ToQuery(Guid speciesId)
    {
        return new GetBreedByIdSpeciesQuery(speciesId, Page, PageSize);
    }
}
