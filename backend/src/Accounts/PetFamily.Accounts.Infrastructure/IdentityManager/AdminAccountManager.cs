using PetFamily.Accounts.Domain.Accounts;

namespace PetFamily.Accounts.Infrastructure.IdentityManager;

public class AdminAccountManager(AccountsDbContext accountsDbContext)
{
    public async Task CreateAdminAccount(AdminAccount adminAccount)
    {
        await accountsDbContext.AdminAccounts.AddAsync(adminAccount);
        await accountsDbContext.SaveChangesAsync();
    }
}