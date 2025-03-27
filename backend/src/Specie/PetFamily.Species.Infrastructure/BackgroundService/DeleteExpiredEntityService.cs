using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core;
using PetFamily.Species.Infrastructure.DbContext;

namespace PetFamily.Species.Infrastructure.BackgroundService;

/*public class DeleteExpiredEntityService
{
    private readonly WriteDbContext _writeDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpiredEntityService(
        WriteDbContext writeDbContext,
        [FromKeyedServices(Modules.Specie)]IUnitOfWork unitOfWork)
    {
        _writeDbContext = writeDbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task Process(int lifetimeAfterDeletion, CancellationToken cancellationToken)
    {
        var species = await _writeDbContext.Species.ToListAsync(cancellationToken);

        foreach (var specie in species)
        {
            if (specie.DeletionDate != null &&
                DateTime.UtcNow >= specie.DeletionDate.Value.AddDays(lifetimeAfterDeletion))
            {
                _writeDbContext.Species.Remove(specie);
                continue;
            }
            
            specie.DeleteExpiredBreed(lifetimeAfterDeletion);
        }
        
        await _unitOfWork.SaveChanges(cancellationToken);
    }
}*/