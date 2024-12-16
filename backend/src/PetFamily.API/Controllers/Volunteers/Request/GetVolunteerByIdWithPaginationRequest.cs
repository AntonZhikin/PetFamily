namespace PetFamily.API.Controllers.Volunteers.Request;

public record GetVolunteerByIdWithPaginationRequest(string? Description, int Page, int PageSize)
{
    public GetVolunteerByIdWithPaginationRequest ToQuery() 
        => new (Description, Page, PageSize);
}