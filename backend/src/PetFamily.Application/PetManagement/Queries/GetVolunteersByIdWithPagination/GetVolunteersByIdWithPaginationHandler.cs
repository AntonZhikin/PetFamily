using Dapper;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Application.Extensions;
using PetFamily.Application.Models;
using PetFamily.Application.PetManagement.Queries.GetVolunteersWithPagination;

namespace PetFamily.Application.PetManagement.Queries.GetVolunteersByIdWithPagination;

public class GetVolunteersByIdWithPaginationHandler 
    : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersByIdWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetVolunteersByIdWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<PagedList<VolunteerDto>> Handle(
        GetVolunteersByIdWithPaginationQuery query, 
        CancellationToken cancellationToken)
    {
        var volunteerQuery = _readDbContext.Volunteers.AsQueryable();

        volunteerQuery = volunteerQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Description),
            i => i.Description.Contains(query.Description!));

        return await volunteerQuery
            .OrderBy(i => i.Description)
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}