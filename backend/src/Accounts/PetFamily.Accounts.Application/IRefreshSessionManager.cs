using CSharpFunctionalExtensions;
using PetFamily.Accounts.Domain;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Application;

public interface IRefreshSessionManager
{
    Task<Result<RefreshSession, Error>> GetByRefreshToken(
        Guid refreshToken, CancellationToken cancellationToken);

    void Delete(RefreshSession refreshSession);
}