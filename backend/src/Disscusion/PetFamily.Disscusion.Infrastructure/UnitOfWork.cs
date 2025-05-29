using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.Core;
using PetFamily.Disscusion.Infrastructure.DbContext;

namespace PetFamily.Disscusion.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly DiscussionsWriteDbContext _dbContext;

    public UnitOfWork(DiscussionsWriteDbContext dbContext)
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