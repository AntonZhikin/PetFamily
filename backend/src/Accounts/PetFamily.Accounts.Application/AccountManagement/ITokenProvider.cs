using System.Security.Claims;
using CSharpFunctionalExtensions;
using PetFamily.Accounts.Application.Models;
using PetFamily.Accounts.Domain;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Application.AccountManagement;

public interface ITokenProvider
{
    Task<JwtTokenResult> GenerateAccessToken(User user, CancellationToken cancellationToken);
    Task<Guid> GenerateRefreshToken(User user, Guid accessTokenJti, CancellationToken cancellationToken);
    Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaims(string jwtToken, CancellationToken cancellationToken);
}