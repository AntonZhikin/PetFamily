using PetFamily.Pets.Application.PetManagement.Queries.GetVolunteersWithPagination;

namespace PetFamily.Pets.Controllers.Volunteers.Request;

public record GetVolunteersWithPaginationRequest(int Page, int PageSize)
{
    public GetVolunteersWithPaginationQuery ToQuery() => 
        new (Page, PageSize);
}
