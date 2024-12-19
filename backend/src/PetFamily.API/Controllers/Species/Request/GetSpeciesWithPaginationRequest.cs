using PetFamily.Application.Species.Queries;

namespace PetFamily.API.Controllers.Species.Request;

public record GetSpeciesWithPaginationRequest(int Page, int PageSize)
{
    public GetSpeciesWithPaginationQuery ToQuery() =>
        new (Page, PageSize);
}
