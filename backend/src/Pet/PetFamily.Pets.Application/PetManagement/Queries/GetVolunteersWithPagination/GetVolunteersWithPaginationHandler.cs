using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs;
using PetFamily.Core.DTOs.Pets;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;

namespace PetFamily.Pets.Application.PetManagement.Queries.GetVolunteersWithPagination;

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
        var result = await _readDbContext.Volunteers.ToPagedList(query.Page, query.PageSize, cancellationToken);
        
        return result;
    }
}