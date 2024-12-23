using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Application.Extensions;
using PetFamily.Application.Models;

namespace PetFamily.Application.Species.Queries.GetSpeciesWithPagination;

public class GetSpeciesWithPaginationHandler 
    : IQueryHandler<PagedList<SpeciesDto>, GetSpeciesWithPaginationQuery>
{
    private readonly ILogger<GetSpeciesWithPaginationHandler> _logger;
    private readonly IReadDbContext _readDbContext;

    public GetSpeciesWithPaginationHandler(
        ILogger<GetSpeciesWithPaginationHandler> logger,
        IReadDbContext readDbContext
        )
    {
        _logger = logger;
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<SpeciesDto>> Handle(
        GetSpeciesWithPaginationQuery query, CancellationToken cancellationToken)
    {
        var speciesQuery = _readDbContext.Species;
        
        var result = await speciesQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
        
        return result;
    }
}