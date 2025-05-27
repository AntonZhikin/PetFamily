using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Queries.GetAllByUserId;

public record GetAllByUserIdQuery(
    Guid UserId,
    string? Status,
    int Page,
    int PageSize) : IQuery;