using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagement.Queries.GetVolunteersByIdWithPagination;

public record GetVolunteersByIdWithPaginationQuery(
    string? Description,
    int Page,
    int PageSize) : IQuery;
