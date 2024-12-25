using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Domain.PetManagement.Entity;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.PetManagement.Queries.GetPetById;

public class GetPetByIdHandler : IQueryHandler<PetDto, GetPetByIdQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetPetByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<PetDto> Handle(
        GetPetByIdQuery query, CancellationToken cancellationToken = default)
    {
        var petDto = await _readDbContext.Pets
            .SingleOrDefaultAsync(p => p.Id == query.PetId, cancellationToken: cancellationToken);
        
        return petDto;
    }
}