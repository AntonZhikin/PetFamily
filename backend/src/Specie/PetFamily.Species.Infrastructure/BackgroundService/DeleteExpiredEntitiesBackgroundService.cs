using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFamily.Core;
using PetFamily.Framework.Authorization;
using PetFamily.Species.Infrastructure.DbContext;

namespace PetFamily.Species.Infrastructure.BackgroundService;

public class DeleteExpiredEntityService
{
    private readonly WriteDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpiredEntityService(
        WriteDbContext context,
        [FromKeyedServices(Modules.Specie)] IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Process(int daysBeforeDelete, CancellationToken cancellationToken)
    {
        var speciesWithBreed = await GetSpeciesWithBreedAsync(cancellationToken);

        foreach (var species in speciesWithBreed)
        {
            species.DeleteExpiredBreed(daysBeforeDelete);
        }

        await _unitOfWork.SaveChanges(cancellationToken);
    }

    private async Task<IEnumerable<Domain.SpeciesManagement.AggregateRoot.Species>> GetSpeciesWithBreedAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Species
            .Include(s => s.Breeds)
            .ToListAsync(cancellationToken);
    }
}