using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.DTOs.Volunteer;
using PetFamily.VolunteerRequest.Application;

namespace PetFamily.VolunteerRequest.Infrastructure.DbContext;

public class VolunteerRequestsReadDbContext :
    Microsoft.EntityFrameworkCore.DbContext, IVolunteerRequestsReadDbContext
{
    private readonly string _connectionString;

    public VolunteerRequestsReadDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IQueryable<VolunteerRequestDto> VolunteerRequests =>
        Set<VolunteerRequestDto>();
    
    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging(false);
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("volunteer_requests");
            
        builder.ApplyConfigurationsFromAssembly(
            typeof(VolunteerRequestsReadDbContext).Assembly,
            type => type.FullName?.Contains("Configuration.Read") ?? false);
    }
}