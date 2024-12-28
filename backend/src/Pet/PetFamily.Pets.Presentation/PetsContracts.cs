using Microsoft.EntityFrameworkCore;
using PetFamily.Core.DTOs;
using PetFamily.Pets.Application;
using PetFamily.Pets.Contracts;
using PetFamily.Pets.Contracts.Request;

namespace PetFamily.Pets.Controllers;

public class PetsContracts : IPetsContracts
{
    private readonly IReadDbContext _readDbContext;

    public PetsContracts(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<PetDto?> AnyPetWithSpeciesId(AnyPetWithSpeciesIdRequest request, CancellationToken cancellationToken = default)
    {
        return await _readDbContext.Pets
            .FirstOrDefaultAsync(x => x.SpeciesBreedDto.SpeciesId == request.SpeciesId, cancellationToken);
    }

    public async Task<PetDto?> AnyPetWithBreedId(AnyPetWithBreedIdRequest request, CancellationToken cancellationToken = default)
    {
        return await _readDbContext.Pets
            .FirstOrDefaultAsync(x => x.SpeciesBreedDto.BreedId == request.BreedId, cancellationToken);
    }
}