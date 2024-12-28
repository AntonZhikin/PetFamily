using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetFamily.Species.Infrastructure.DbContext;

public class WriteDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly string _connectionString;

    public WriteDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public DbSet<Domain.SpeciesManagement.AggregateRoot.Species> Species => 
        Set<Domain.SpeciesManagement.AggregateRoot.Species>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteDbContext).Assembly, 
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
    
    private static ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
    
}