using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Application.AccountManagement.Commands.Logout;

public class LogoutHandler : ICommandHandler<LogoutCommand>
{
    private readonly IRefreshSessionManager _refreshSessionManager;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutHandler(
        [FromKeyedServices(Modules.Accounts)]IUnitOfWork unitOfWork, 
        IRefreshSessionManager refreshSessionManager)
    {
        _unitOfWork = unitOfWork;
        _refreshSessionManager = refreshSessionManager;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(LogoutCommand command, CancellationToken cancellationToken = default)
    {
        var oldRefreshSession = await _refreshSessionManager
            .GetByRefreshToken(command.RefreshToken, cancellationToken);

        if (oldRefreshSession.IsFailure)
            return oldRefreshSession.Error.ToErrorList();

        _refreshSessionManager.Delete(oldRefreshSession.Value);
        await _unitOfWork.SaveChanges(cancellationToken);

        return UnitResult.Success<ErrorList>();
    }
}