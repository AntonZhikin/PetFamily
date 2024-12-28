using PetFamily.Core.Abstractions;

namespace PetFamily.Species.Application.Species.Queries.GetBreedByIdSpecies;

public record GetBreedByIdSpeciesQuery(Guid SpeciesId, int Page, int PageSize) : IQuery;
