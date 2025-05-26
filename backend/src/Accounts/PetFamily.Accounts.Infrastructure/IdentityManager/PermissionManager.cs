using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;

namespace PetFamily.Accounts.Infrastructure.IdentityManager;

public class PermissionManager(WriteAccountsDbContext writeAccountsDbContext)
{
    public async Task<Permission?> FindByCode(string code) 
        => await writeAccountsDbContext.Permissions.FirstOrDefaultAsync(x => x.Code == code);

    public async Task AddRangeIfExist(IEnumerable<string> permissionsToAdd, CancellationToken cancellationToken = default)
    {
        foreach (var permissionCode in permissionsToAdd)
        {
            var isPermissionExist = await writeAccountsDbContext.Permissions
                .AnyAsync(p => p.Code == permissionCode, cancellationToken: cancellationToken);
            
            if (isPermissionExist)
                return;
            
            await writeAccountsDbContext.Permissions.AddAsync(new Permission {Code = permissionCode}, cancellationToken);
        }
        
        await writeAccountsDbContext.SaveChangesAsync(cancellationToken);
        
    }

    public async Task<HashSet<string>> GetUserPermissions(Guid userId)
    {
        var permissions = await writeAccountsDbContext.Users
            .Include(u => u.Roles)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .SelectMany(r => r.RolePermissions)
            .Select(rp => rp.Permission.Code)
            .ToListAsync();
        
        return permissions.ToHashSet();
    }
    
    public async Task<HashSet<string>> GetUserRole(Guid userId)
    {
        var permissions = await writeAccountsDbContext.Users
            .Include(u => u.Roles)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .SelectMany(r => r.RolePermissions)
            .Select(rp => rp.Permission.Code)
            .ToListAsync();
        
        return permissions.ToHashSet();
    }
}