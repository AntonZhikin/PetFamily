using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain.DataModels;

namespace PetFamily.Accounts.Infrastructure;

public class RolePermissionManager(AccountsDbContext accountsDbContext)
{
    public async Task AddRangeIfExist(Guid roleId, IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var permission = await accountsDbContext.Permissions
                .FirstOrDefaultAsync(x => x.Code == permissionCode);
            if (permission == null)
                throw new ApplicationException($"Permission code {permissionCode} is not found");
                
            var rolePermissionExist = await accountsDbContext.RolePermissions
                .AnyAsync(x => x.RoleId == roleId && x.PermissionId == permission!.Id);
                
            if (rolePermissionExist)
                continue;

            accountsDbContext.RolePermissions.Add(new RolePermission 
            {
                RoleId = roleId,
                PermissionId = permission!.Id 
            });
        }
        
        await accountsDbContext.SaveChangesAsync();
        
    }
}