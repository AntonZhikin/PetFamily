using PetFamily.Application.Abstractions;

namespace PetFamily.Application.Species.Queries.GetBreedByIdSpecies;

public record GetBreedByIdSpeciesQuery(Guid SpeciesId, int Page, int PageSize) : IQuery;
