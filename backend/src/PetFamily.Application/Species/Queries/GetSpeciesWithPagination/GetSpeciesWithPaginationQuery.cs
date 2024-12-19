using PetFamily.Application.Abstractions;
using PetFamily.Application.DTOs;

namespace PetFamily.Application.Species.Queries;

public record GetSpeciesWithPaginationQuery(int Page, int PageSize) : IQuery;