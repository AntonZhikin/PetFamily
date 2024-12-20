using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Application.Models;
using PetFamily.Domain.PetManagement.AggregateRoot;
using PetFamily.Domain.PetManagement.Ids;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.PetManagement.Queries.GetVolunteerByIdQuery;

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