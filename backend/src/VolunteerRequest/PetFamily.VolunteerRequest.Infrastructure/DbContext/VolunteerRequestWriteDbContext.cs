using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetFamily.VolunteerRequest.Infrastructure.DbContext;

public class VolunteerRequestsWriteDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly string _connectionString;
        
    public DbSet<Domain.VolunteerRequest> VolunteerRequests => Set<Domain.VolunteerRequest>();

    public VolunteerRequestsWriteDbContext(string connectionString)
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
        builder.HasDefaultSchema("PetFamily_VolunteerRequest");

        builder.ApplyConfigurationsFromAssembly(
            typeof(VolunteerRequestsWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configuration.Write") ?? false);
    }
    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}