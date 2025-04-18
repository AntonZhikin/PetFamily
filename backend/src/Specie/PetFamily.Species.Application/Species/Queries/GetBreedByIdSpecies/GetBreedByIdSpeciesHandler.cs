using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs;
using PetFamily.Core.DTOs.Species;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;

namespace PetFamily.Species.Application.Species.Queries.GetBreedByIdSpecies;

public class GetBreedByIdSpeciesHandler : IQueryHandler<PagedList<BreedDto>, GetBreedByIdSpeciesQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetBreedByIdSpeciesHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<PagedList<BreedDto>> Handle(
        GetBreedByIdSpeciesQuery query, CancellationToken cancellationToken = default)
    {
        var speciesQuery = _readDbContext.Breed.AsQueryable();
        
        speciesQuery = speciesQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.SpeciesId.ToString()), x => x.SpeciesId == query.SpeciesId);
        
        var pagedList = await speciesQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
        
        return pagedList;
    }
}