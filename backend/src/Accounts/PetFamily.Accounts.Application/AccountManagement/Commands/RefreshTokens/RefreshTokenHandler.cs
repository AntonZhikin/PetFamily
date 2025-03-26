using System.Security.Claims;
using System.Windows.Input;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Contracts.Responses;
using PetFamily.Accounts.Domain;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Application.AccountManagement.Commands.RefreshTokens;

public class RefreshTokenHandler : ICommandHandler<LoginResponse, RefreshTokenCommand>
{
    private readonly IRefreshSessionManager _refreshSessionManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenHandler(
        IRefreshSessionManager refreshSessionManager, 
        ITokenProvider tokenProvider,
        [FromKeyedServices(Modules.Accounts)]IUnitOfWork unitOfWork)
    {
        _refreshSessionManager = refreshSessionManager;
        _tokenProvider = tokenProvider;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<LoginResponse, ErrorList>> Handle(
        RefreshTokenCommand command, CancellationToken cancellationToken = default)
    {
        var oldRefreshSession = await _refreshSessionManager
            .GetByRefreshToken(command.RefreshToken, cancellationToken);
        
        if (oldRefreshSession.IsFailure)
            return oldRefreshSession.Error.ToErrorList();

        if (oldRefreshSession.Value.ExpiresIn < DateTime.UtcNow)
        {
            return Errors.Tokens.ExpiredToken().ToErrorList();
        }
            
        _refreshSessionManager.Delete(oldRefreshSession.Value);
        await _unitOfWork.SaveChanges(cancellationToken);

        var accessToken = await _tokenProvider
            .GenerateAccessToken(oldRefreshSession.Value.User, cancellationToken);
        
        var refreshToken = await _tokenProvider
            .GenerateRefreshToken(oldRefreshSession.Value.User, accessToken.Jti, cancellationToken);
        
        return new LoginResponse(
            accessToken.AccessToken, 
            refreshToken, 
            oldRefreshSession.Value.User.Id,
            oldRefreshSession.Value.User.Email!);
    }
}