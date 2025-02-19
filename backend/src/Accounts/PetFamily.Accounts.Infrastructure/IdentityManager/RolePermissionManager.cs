using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;

namespace PetFamily.Accounts.Infrastructure.IdentityManager;

public class RolePermissionManager(WriteAccountsDbContext writeAccountsDbContext)
{
    public async Task AddRangeIfExist(Guid roleId, IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var permission = await writeAccountsDbContext.Permissions
                .FirstOrDefaultAsync(x => x.Code == permissionCode);
            if (permission == null)
                throw new ApplicationException($"Permission code {permissionCode} is not found");
                
            var rolePermissionExist = await writeAccountsDbContext.RolePermissions
                .AnyAsync(x => x.RoleId == roleId && x.PermissionId == permission!.Id);
                
            if (rolePermissionExist)
                continue;

            writeAccountsDbContext.RolePermissions.Add(new RolePermission 
            {
                RoleId = roleId,
                PermissionId = permission!.Id 
            });
        }
        
        await writeAccountsDbContext.SaveChangesAsync();
        
    }
}