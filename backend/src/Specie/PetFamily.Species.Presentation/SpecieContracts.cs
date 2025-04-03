using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.DTOs;
using PetFamily.Core.DTOs.Species;
using PetFamily.Species.Application;
using PetFamily.Species.Contracts;
using PetFamily.Species.Contracts.Request;

namespace PetFamily.Species.Presentation;

public class SpecieContracts : ISpecieContract
{
    private readonly IReadDbContext _readDbContext;

    public SpecieContracts(IReadDbContext dbContext)
    {
        _readDbContext = dbContext;
    }

    public async Task<SpeciesDto?> GetSpeciesById(
        GetSpecieByIdRequest request, CancellationToken cancellationToken)
    {
        return await _readDbContext.Species
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    }
    
    public async Task<BreedDto?> GetBreedById(
        GetBreedByIdRequest request, CancellationToken cancellationToken)
    {
        return await _readDbContext.Breed
            .FirstOrDefaultAsync(x => x.SpeciesId == request.SpecieId 
                && x.Id == request.BreedId, cancellationToken);
    }
}