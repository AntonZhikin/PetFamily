using PetFamily.Accounts.Domain.Accounts;
using PetFamily.Accounts.Infrastructure.DbContexts;

namespace PetFamily.Accounts.Infrastructure.IdentityManager;

public class AdminAccountManager(WriteAccountsDbContext writeAccountsDbContext)
{
    public async Task CreateAdminAccount(AdminAccount adminAccount)
    {
        await writeAccountsDbContext.AdminAccounts.AddAsync(adminAccount);
        await writeAccountsDbContext.SaveChangesAsync();
    }
}   