using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Queries.GetAllByAdminId;

public record GetAllByAdminIdQuery(
    Guid AdminId,
    string? Status,
    int Page,
    int PageSize) : IQuery;