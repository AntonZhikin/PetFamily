using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFamily.Core;

namespace PetFamily.Species.Infrastructure.BackgroundService;

/*public class DeleteExpiredEntitiesBackgroundService : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly ILogger<DeleteExpiredEntitiesBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ExpiredEntitiesDeletionOptions _options;

    public DeleteExpiredEntitiesBackgroundService(
        ILogger<DeleteExpiredEntitiesBackgroundService> logger,
        IServiceScopeFactory scopeFactory,
        IOptions<ExpiredEntitiesDeletionOptions> options)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("DeleteExpiredEntitiesBackgroundService is starting to work");
        
        while (!cancellationToken.IsCancellationRequested)
        {
            await using var scope = _scopeFactory.CreateAsyncScope();
            
            var deleteExpiredEntitiesService = scope.ServiceProvider
                .GetRequiredService<DeleteExpiredEntityService>();
            
            _logger.LogInformation("DeleteExpiredEntitiesBackgroundService is deleting expired entities");
            
            await deleteExpiredEntitiesService.Process(_options.EntityExpiredDaysTime, cancellationToken);
            
            await Task.Delay(TimeSpan.FromHours(_options.WorkHoursInterval), cancellationToken);
            
        }
        
        _logger.LogInformation("DeleteExpiredEntitiesBackgroundService is stopping to work");
    }
}*/