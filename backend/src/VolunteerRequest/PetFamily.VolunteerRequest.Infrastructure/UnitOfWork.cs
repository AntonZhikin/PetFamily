using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.Core;
using PetFamily.VolunteerRequest.Infrastructure.DbContext;

namespace PetFamily.VolunteerRequest.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly VolunteerRequestsWriteDbContext _dbContext;

    public UnitOfWork(VolunteerRequestsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IDbTransaction> BeginTransaction(
        CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext
            .Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(
        CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}