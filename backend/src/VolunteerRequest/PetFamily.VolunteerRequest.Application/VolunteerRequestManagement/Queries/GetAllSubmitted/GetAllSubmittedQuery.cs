using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Queries.GetAllSubmitted;

public record GetAllSubmittedQuery(
    int Page,
    int PageSize) : IQuery;