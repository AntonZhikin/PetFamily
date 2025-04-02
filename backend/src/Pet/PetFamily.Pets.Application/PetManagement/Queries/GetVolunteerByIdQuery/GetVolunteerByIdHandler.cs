using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs;
using PetFamily.Core.DTOs.Pets;

namespace PetFamily.Pets.Application.PetManagement.Queries.GetVolunteerByIdQuery;

public class GetVolunteerByIdHandler : IQueryHandler<VolunteerDto, GetVolunteerByIdQuery>
{
    private readonly IReadDbContext _readDbContext;


    public GetVolunteerByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<VolunteerDto> Handle(
        GetVolunteerByIdQuery query, 
        CancellationToken cancellationToken)
    {
        var volunteerDto = await _readDbContext.Volunteers
            .SingleOrDefaultAsync(v => v.Id == query.VolunteerId, cancellationToken);
        
        return volunteerDto;
    }
}