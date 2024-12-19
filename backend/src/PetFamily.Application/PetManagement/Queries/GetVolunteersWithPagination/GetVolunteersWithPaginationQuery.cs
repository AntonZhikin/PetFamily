using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagement.Queries.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(int Page, int PageSize) : IQuery;