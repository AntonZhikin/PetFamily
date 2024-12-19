using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Application.Extensions;
using PetFamily.Application.Models;
using PetFamily.Domain.PetManagement.AggregateRoot;

namespace PetFamily.Application.PetManagement.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler 
    : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<PagedList<VolunteerDto>> Handle(
        GetVolunteersWithPaginationQuery query, 
        CancellationToken cancellationToken)
    {
        var volunteerQuery = _readDbContext.Volunteers.AsQueryable();

        var result = await volunteerQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
        
        return result;
    }
}