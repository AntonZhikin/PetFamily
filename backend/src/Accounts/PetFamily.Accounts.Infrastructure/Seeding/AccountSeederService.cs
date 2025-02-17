using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFamily.Accounts.Domain.DataModels;

namespace PetFamily.Accounts.Infrastructure.Seeding;

public class AccountSeederService(
    UserManager<User> userManager, 
    RoleManager<Role> roleManager,
    PermissionManager permissionManager,
    RolePermissionManager rolePermissionManager,
    IOptions<AdminOptions> adminOptions,
    ILogger<AccountSeederService> logger)
{
    private readonly AdminOptions _adminOptions = adminOptions.Value;
    
    public async Task SeedAsync()
    {
        var json = await File.ReadAllTextAsync("etc/accounts.json");
        
        var seedData = JsonSerializer.Deserialize<RolePermissionConfig>(json)
                       ?? throw new ApplicationException("Invalid JSON");
        
        await SeedPermissions(seedData);

        await SeedRoles(seedData);

        await SeedRolePermision(seedData); 
        
        var adminUser = new User
        {
            UserName = _adminOptions.UserName, 
            Email = _adminOptions.Email,
        };
        
        await userManager.CreateAsync(adminUser, _adminOptions.Password);
        
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
    
    private async Task SeedRolePermision(RolePermissionConfig seedData)
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
}