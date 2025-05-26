using PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Queries.GetAllSubmitted;

namespace PetFamily.VolunteerRequest.Presentation.Request;

public record GetAllSubmittedRequest(
    int Page, int PageSize)
{
    public GetAllSubmittedQuery ToQuery() =>
        new(Page, PageSize);
}