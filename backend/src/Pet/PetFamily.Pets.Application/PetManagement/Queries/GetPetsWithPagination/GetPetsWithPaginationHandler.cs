using System.Linq.Expressions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;

namespace PetFamily.Pets.Application.PetManagement.Queries.GetPetsWithPagination;

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
        var petQuery = _readDbContext.Pets.AsQueryable();

        Expression<Func<PetDto, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "name" => (pet) => pet.Name,
            "color" => (pet) => pet.Color,
            "VolunteerId" => (pet) => pet.VolunteerId,
            "SpeciesId" => (pet) => pet.SpeciesBreedDto.SpeciesId,
            "BreedId" => (pet) => pet.SpeciesBreedDto.BreedId,
            _ => p => p.Id
        };
        
        petQuery = query.SortDirection?.ToLower() == "desc" 
            ? petQuery.OrderByDescending(keySelector) 
            : petQuery.OrderBy(keySelector);
        
        petQuery = petQuery.WhereIf(query.VolunteerId != null,
            p => p.VolunteerId == query.VolunteerId);
        
        petQuery = petQuery.WhereIf(
            !string.IsNullOrEmpty(query.Name),
            p => p.Name == query.Name);

        petQuery = petQuery.WhereIf(
            query.IsVaccine != null,
            p => p.IsVaccine == query.IsVaccine);
        
        petQuery = petQuery.WhereIf(
            query.IsNeutered != null,
            p => p.IsNeutered == query.IsNeutered);
        
        petQuery = petQuery.WhereIf(query.SpeciesId != null,
            p => p.SpeciesBreedDto.SpeciesId == query.SpeciesId);
        
        petQuery = petQuery.WhereIf(query.BreedId != null,
            p => p.SpeciesBreedDto.BreedId == query.BreedId);
        
        petQuery = petQuery.WhereIf(
            !string.IsNullOrEmpty(query.Color),
            p => p.Color == query.Color);
        
        return petQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}