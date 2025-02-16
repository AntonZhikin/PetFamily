using System.Formats.Asn1;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Domain.DataModels;

namespace PetFamily.Accounts.Infrastructure;

public class AccountsSeeder
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<AccountsSeeder> _logger;

    public AccountsSeeder(IServiceScopeFactory serviceScopeFactory, ILogger<AccountsSeeder> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        _logger.LogInformation("Seeding accounts...");
        
        var json = await File.ReadAllTextAsync("etc/accounts.json");

        _logger.LogInformation(json);
        
        using var scope = _serviceScopeFactory.CreateScope();
        
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        var permissionManager = scope.ServiceProvider.GetRequiredService<PermissionManager>();

        var seedData = JsonSerializer.Deserialize<RolePermissionConfig>(json)
            ?? throw new ApplicationException("Invalid JSON");
        
        await SeedPermissions(seedData, permissionManager);
    }

    private async Task SeedPermissions(RolePermissionConfig seedData, PermissionManager permissionManager)
    {
        var permissionsToAdd = seedData.Permissions
            .SelectMany(permissionsGroup => permissionsGroup.Value);

        await permissionManager.AddRangeIfExist(permissionsToAdd);
        
        _logger.LogInformation("Permissions added to database.");
    }
}

public class RolePermissionConfig 
{
    public Dictionary<string, string[]> Permissions { get; set; } = [];
    public Dictionary<string, string[]> Roles { get; set; } = [];
}