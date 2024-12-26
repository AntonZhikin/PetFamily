using System.Linq.Expressions;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Application.Extensions;
using PetFamily.Application.Models;

namespace PetFamily.Application.PetManagement.Queries.GetPetsWithPagination;

public class GetPetsWithPaginationHandler : IQueryHandler<PagedList<PetDto>, GetPetsWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetPetsWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public Task<PagedList<PetDto>> Handle(GetPetsWithPaginationQuery query, 
        CancellationToken cancellationToken = default)
    {
        var petQuery = _readDbContext.Pets;

        Expression<Func<PetDto, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "Name" => p => p.Name,
            "Color" => p => p.Color,
            "VolunteerId" => p => p.VolunteerId,
            "SpeciesId" => p => p.SpeciesBreedDto.SpeciesId,
            "BreedId" => p => p.SpeciesBreedDto.BreedId,
            _ => p => p.Id
        };
        
        petQuery = query.SortDirection?.ToLower() == "desc" 
            ? petQuery.OrderByDescending(keySelector) 
            : petQuery.OrderBy(keySelector);
        
        petQuery = petQuery.OrderBy(keySelector);

        return petQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}