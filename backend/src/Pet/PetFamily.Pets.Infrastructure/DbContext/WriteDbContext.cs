using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Pets.Domain.AggregateRoot;
using PetFamily.Pets.Domain.Entity;

namespace PetFamily.Pets.Infrastructure.DbContext;

public class WriteDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly string _connectionString;

    public WriteDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Pet> Pets => Set<Pet>();
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