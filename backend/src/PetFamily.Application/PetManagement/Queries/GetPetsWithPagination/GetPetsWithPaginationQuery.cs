using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagement.Queries.GetPetsWithPagination;

public record GetPetsWithPaginationQuery(
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    string? Name,
    string? Color,
    Guid? VolunteerId,
    Guid? SpeciesId,
    Guid? BreedId,
    bool? IsNeutered,
    bool? IsVaccine
) : IQuery;
    
