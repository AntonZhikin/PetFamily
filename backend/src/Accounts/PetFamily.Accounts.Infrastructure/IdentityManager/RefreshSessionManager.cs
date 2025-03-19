using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Infrastructure.IdentityManager;

public class RefreshSessionManager(WriteAccountsDbContext writeAccountsDbContext) : IRefreshSessionManager
{
    public async Task<Result<RefreshSession, Error>> GetByRefreshToken(
        Guid refreshToken, CancellationToken cancellationToken)
    {
        var refreshSession = await writeAccountsDbContext.RefreshSessions
            .Include(r => r.User)
            .FirstOrDefaultAsync(a => a.RefreshToken == refreshToken, cancellationToken);
        
        if (refreshSession == null)
            return Errors.General.NotFound(refreshToken);
        
        return refreshSession;
    }
    
    public void Delete(RefreshSession refreshSession)
    {
        writeAccountsDbContext.RefreshSessions.Remove(refreshSession);
    }
}