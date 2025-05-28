using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetFamily.Disscusion.Infrastructure.DbContext;

public class DiscussionsWriteDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly string _connectionString;
    
    public DbSet<Domain.AggregateRoot.Discussion> Discussions => Set<Domain.AggregateRoot.Discussion>();
    
    public DiscussionsWriteDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging(false);
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("discussions");

        builder.ApplyConfigurationsFromAssembly(
            typeof(DiscussionsWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configuration.Write") ?? false);
    }
    
    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}