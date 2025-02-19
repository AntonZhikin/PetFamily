using PetFamily.Accounts.Contracts;
using PetFamily.Accounts.Infrastructure.IdentityManager;

namespace PetFamily.Accounts.Presentation;

public class AccountsContract : IAccountsContract
{
    private readonly PermissionManager _permissionManager;

    public AccountsContract(PermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }
    
    public async Task<HashSet<string>> GetUserPermissionsCodes(Guid userId)
    {
        return await _permissionManager.GetUserPermissions(userId);
    }
}