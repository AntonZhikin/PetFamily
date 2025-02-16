using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain.DataModels;

namespace PetFamily.Accounts.Infrastructure;

public class PermissionManager(AccountsDbContext accountsDbContext)
{
    public async Task<Permission?> FindByCode(string code) 
        => await accountsDbContext.Permissions.FirstOrDefaultAsync(x => x.Code == code);

    public async Task AddRangeIfExist(IEnumerable<string> permissionsToAdd)
    {
        foreach (var permissionCode in permissionsToAdd)
        {
            var isPermissionExist = await accountsDbContext.Permissions
                .AnyAsync(p => p.Code == permissionCode);
            
            if (isPermissionExist)
                return;
            
            await accountsDbContext.Permissions.AddAsync(new Permission {Code = permissionCode});
        }
        
        await accountsDbContext.SaveChangesAsync();
        
    }
}