using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Contracts;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;
using PetFamily.Accounts.Infrastructure.IdentityManager;
using PetFamily.Core;

namespace PetFamily.Accounts.Presentation;

public class AccountsContract : IAccountsContract
{
    private readonly PermissionManager _permissionManager;
    private readonly WriteAccountsDbContext _accountsWriteDbContext;
    private readonly IAccountsReadDbContext _accountsReadDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AccountsContract> _logger;

    public AccountsContract(
        PermissionManager permissionManager,
        WriteAccountsDbContext accountsWriteDbContext,
        IAccountsReadDbContext accountsReadDbContext,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork,
        ILogger<AccountsContract> logger)
    {
        _permissionManager = permissionManager;
        _accountsWriteDbContext = accountsWriteDbContext;
        _accountsReadDbContext = accountsReadDbContext;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<HashSet<string>> GetUserPermissionsCodes(Guid userId)
    {
        return await _permissionManager.GetUserPermissions(userId);
    }

    public async Task BanUser(Guid userId, CancellationToken cancellationToken)
    {
        var participantAccount = await _accountsWriteDbContext.ParticipantAccounts
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        var until = DateTime.UtcNow.AddDays(7);

        if (participantAccount == null) return;

        participantAccount.BanForRequestsForWeek(until);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("User {userId} banned for requests until {until}",
            userId, until);
    }
    
    public async Task<bool> IsUserBannedForVolunteerRequests(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var userDto = await _accountsReadDbContext.ParticipantAccounts
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        return DateTime.UtcNow < userDto!.BannedForRequestsUntil;
    }
}