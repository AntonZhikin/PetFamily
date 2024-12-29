using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs;

namespace PetFamily.Pets.Application.PetManagement.Queries.GetPetById;

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