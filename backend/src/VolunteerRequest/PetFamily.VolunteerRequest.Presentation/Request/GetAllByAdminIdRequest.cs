using PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Queries.GetAllByAdminId;

namespace PetFamily.VolunteerRequest.Presentation.Request;

public record GetAllByAdminIdRequest(
    string? Status,
    int Page,
    int PageSize)
{
    public GetAllByAdminIdQuery ToQuery(Guid adminId) =>
        new(adminId, Status, Page, PageSize);
}