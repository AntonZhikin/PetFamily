using Microsoft.EntityFrameworkCore;
using PetFamily.Kernel;
using PetFamily.Species.Infrastructure.DbContext;

namespace PetFamily.Species.Infrastructure.Services;

public class DeleteExpiredBreedService
{
    private readonly WriteDbContext _writeDbContext;

    public DeleteExpiredBreedService(
        WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task Process(CancellationToken cancellationToken)
    {
        var species = await GetSpeciesWithBreedAsync(cancellationToken);

        foreach (var specie in species)
        {
            specie.DeleteExpiredBreed();
        }

        await _writeDbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<List<Domain.SpeciesManagement.AggregateRoot.Species>> GetSpeciesWithBreedAsync(CancellationToken cancellationToken)
    {
        return await _writeDbContext.Species
            .Include(m => m.Breeds)
            .ToListAsync(cancellationToken);
    }
}