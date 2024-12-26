using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;

namespace PetFamily.Infrastructure.DbContext;

public class ReadDbContext(string ConnectionString) : 
    Microsoft.EntityFrameworkCore.DbContext, 
    IReadDbContext
{
    private readonly string _connectionString;

    // public ReadDbContext(string connectionString)
    // {
    //     _connectionString = connectionString;
    // }
    
    public IQueryable<VolunteerDto> Volunteers => Set<VolunteerDto>();
    public IQueryable<PetDto> Pets => Set<PetDto>();
    public IQueryable<SpeciesDto> Species => Set<SpeciesDto>();
    public IQueryable<BreedDto> Breed => Set<BreedDto>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReadDbContext).Assembly, 
            type => type.FullName?.Contains("Configurations.Read") ?? false);
        
    }

    private static ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}