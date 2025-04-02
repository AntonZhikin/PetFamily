using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Application;
using PetFamily.Core.DTOs.Accounts;

namespace PetFamily.Accounts.Infrastructure.DbContexts;

public class ReadAccountsDbContext : DbContext, IAccountsReadDbContext
{
    private readonly string _connectionString;

    public ReadAccountsDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IQueryable<UserDto> Users => Set<UserDto>(); 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("PetFamily_Accounts");
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReadAccountsDbContext).Assembly,
            x => x.FullName!.Contains("Configurations.Read"));
    }
    
    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => {builder.AddConsole();});
}