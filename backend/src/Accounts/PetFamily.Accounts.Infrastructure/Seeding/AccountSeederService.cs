using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Domain.Accounts;
using PetFamily.Accounts.Infrastructure.IdentityManager;
using PetFamily.Accounts.Infrastructure.Options;
using PetFamily.Kernel.ValueObject;

namespace PetFamily.Accounts.Infrastructure.Seeding;

public class AccountSeederService(
    UserManager<User> userManager, 
    RoleManager<Role> roleManager,
    PermissionManager permissionManager,
    AccountsManagers accountsManagers,
    RolePermissionManager rolePermissionManager,
    IOptions<AdminOptions> adminOptions,
    ILogger<AccountSeederService> logger)
{
    private readonly AdminOptions _adminOptions = adminOptions.Value;
    
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var json = await File.ReadAllTextAsync("etc/accounts.json");
        
        var seedData = JsonSerializer.Deserialize<RolePermissionConfig>(json)
                       ?? throw new ApplicationException("Invalid JSON");
        
        await SeedPermissions(seedData);

        await SeedRoles(seedData);

        await SeedRolePermission(seedData);
        
        await SeedAdmin(cancellationToken);
        //
       
        
        
        // var adminRole = await roleManager.FindByNameAsync(AdminAccount.RoleName) 
        //                 ?? throw new ApplicationException("Invalid ADMIN");
        //
        // var adminUser = User.CreateAdmin(_adminOptions.UserName, _adminOptions.Email, adminRole);
        // await userManager.CreateAsync(adminUser, _adminOptions.Password);
        //
        // var name = Name.Create(_adminOptions.UserName).Value;
        // var adminAccount = new AdminAccount(adminUser);
        //
        // await accountsManagers.CreateAdminAccount(adminAccount);
    }   
    
    private async Task SeedRolePermission(RolePermissionConfig seedData)
    {
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            var rolePermissions = seedData.Roles[roleName];
            
            await rolePermissionManager.AddRangeIfExist(role!.Id, rolePermissions);
        }
        
        logger.LogInformation("Role permissions add database");
    }

    private async Task SeedRoles(RolePermissionConfig seedData)
    {
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (role is null)
            {
                await roleManager.CreateAsync(new Role { Name = roleName });
            }
        }
        
        logger.LogInformation("Roles added to database.");
    }

    private async Task SeedPermissions(RolePermissionConfig seedData)
    {
        var permissionsToAdd = seedData.Permissions
            .SelectMany(permissionsGroup => permissionsGroup.Value);

        await permissionManager.AddRangeIfExist(permissionsToAdd);
        
        logger.LogInformation("Permissions added to database.");
    }
    
    private async Task SeedAdmin(CancellationToken cancellationToken)
    {
        /*var adminRole = await roleManager.FindByNameAsync(AdminAccount.RoleName)
                        ?? throw new ApplicationException("Role admin couldn't be found");
        
        var adminExist = await userManager.Users
            .AnyAsync(u => u.UserName == AdminAccount.RoleName, cancellationToken);
        if(adminExist) return;
        
        var adminUserResult = User.Create(
            _adminOptions.UserName, _adminOptions.Email, adminRole);
        if (adminUserResult.IsFailure) return;
        
        await userManager.CreateAsync(adminUserResult.Value, _adminOptions.Password);

        var adminAccount = new AdminAccount(adminUserResult.Value);
        await accountsManagers.CreateAdminAccount(adminAccount, cancellationToken);
        adminUserResult.Value.AdminAccount = adminAccount;*/
        
        
        var adminRole = await roleManager.FindByNameAsync(AdminAccount.RoleName) 
                        ?? throw new ApplicationException("Invalid ADMIN");
        
        if (await accountsManagers.AnyAdminAccountExists(cancellationToken)) return;
        
        var adminUser = User.CreateAdmin(_adminOptions.UserName, _adminOptions.Email, adminRole);
        
        await userManager.CreateAsync(adminUser, _adminOptions.Password);
        
        var adminAccount = new AdminAccount(adminUser);
        
        await accountsManagers.CreateAdminAccount(adminAccount, cancellationToken);
    }
}