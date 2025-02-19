using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Domain.Accounts;

namespace PetFamily.Accounts.Infrastructure.DbContexts;

public class WriteAccountsDbContext : IdentityDbContext<User, Role, Guid>
{
    private readonly string _connectionString;
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<Permission> Permissions => Set<Permission>();
    
    public DbSet<AdminAccount> AdminAccounts => Set<AdminAccount>();
    public DbSet<ParticipantAccount> ParticipantAccounts => Set<ParticipantAccount>();
    public DbSet<VolunteerAccount> VolunteerAccounts => Set<VolunteerAccount>();
    public DbSet<User> Users => Set<User>();
    
    public WriteAccountsDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .ToTable("users");

        modelBuilder.Entity<Role>()
            .ToTable("roles");
        
        modelBuilder.Entity<IdentityUserClaim<Guid>>()
            .ToTable("user_claims");
        
        modelBuilder.Entity<IdentityUserToken<Guid>>()
            .ToTable("user_tokens");
        
        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .ToTable("user_logins");
        
        modelBuilder.Entity<IdentityRoleClaim<Guid>>()
            .ToTable("role_claims");
        
        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .ToTable("user_roles");

        modelBuilder.Entity<Permission>()
            .ToTable("permissions");
        
        modelBuilder.Entity<Permission>()
            .HasIndex(u => u.Code)
            .IsUnique();

        modelBuilder.Entity<Permission>()
            .HasIndex(u => u.Code)
            .IsUnique();

        modelBuilder.Entity<RolePermission>()
            .ToTable("role_permissions");
        
        modelBuilder.Entity<RolePermission>()
            .HasKey(r => new { r.RoleId, r.PermissionId });
        
        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);
        
        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId);

        modelBuilder.ApplyConfigurationsFromAssembly(
        typeof(WriteAccountsDbContext).Assembly, 
        type => type.FullName?.Contains("Configurations.Write") ?? false);
        
        modelBuilder.HasDefaultSchema("PetFamily_Accounts");
    }
    
    private static ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
    }
}