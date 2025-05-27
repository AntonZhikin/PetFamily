using PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Queries.GetAllByUserId;

namespace PetFamily.VolunteerRequest.Presentation.Request;

public record GetAllByUserIdRequest(
    string? Status,
    int Page,
    int PageSize)
{
    public GetAllByUserIdQuery ToQuery(Guid userId) =>
        new(userId, Status, Page, PageSize);
}