using PetFamily.Pets.Application.PetManagement.Queries.GetPetsWithPagination;

namespace PetFamily.Pets.Controllers.Volunteers.Request;

public record GetPetsWithPaginationRequest(
    int Page, 
    int PageSize, 
    string? SortBy,
    string? SortDirection,
    Guid? VolunteerId,
    Guid? BreedId,
    Guid? SpeciesId,
    string? Name, 
    string? Color,
    bool? IsNeutered,
    bool? IsVaccine)
{
    public GetPetsWithPaginationQuery ToQuery() =>
        new GetPetsWithPaginationQuery(
            Page, 
            PageSize,
            SortBy, 
            SortDirection,
            Name,
            Color,
            VolunteerId,
            SpeciesId,
            BreedId,
            IsNeutered,
            IsVaccine);
}