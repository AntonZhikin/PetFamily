using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.DTOs;
using PetFamily.Core.DTOs.Pets;
using PetFamily.Core.DTOs.Species;
using PetFamily.Species.Application;

namespace PetFamily.Species.Infrastructure.DbContext;

public class ReadDbContext(string ConnectionString) 
    : Microsoft.EntityFrameworkCore.DbContext, IReadDbContext
{
    public IQueryable<SpeciesDto> Species => Set<SpeciesDto>();
    public IQueryable<BreedDto> Breed => Set<BreedDto>();
    
    public IQueryable<PetDto> Pets => Set<PetDto>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(ConnectionString)
            .UseLoggerFactory(CreateLoggerFactory)
            .EnableSensitiveDataLogging()
            .UseSnakeCaseNamingConvention();

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("species");
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReadDbContext).Assembly, 
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }

    private static readonly ILoggerFactory CreateLoggerFactory
        = LoggerFactory.Create(builder => { builder.AddConsole(); });
} 