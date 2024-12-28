using PetFamily.Core.Abstractions;

namespace PetFamily.Pets.Application.PetManagement.Queries.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(int Page, int PageSize) : IQuery;