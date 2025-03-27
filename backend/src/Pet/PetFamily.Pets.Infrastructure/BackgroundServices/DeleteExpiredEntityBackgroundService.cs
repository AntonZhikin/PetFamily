using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFamily.Core;
using PetFamily.Kernel;

namespace PetFamily.Pets.Infrastructure.BackgroundServices;

public class DeleteExpiredEntitiesBackgroundService : BackgroundService
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
        while (!cancellationToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<DeleteExpiredEntitiesBackgroundService>>();
            logger.LogInformation("Expired pet cleaner is running.");

            var removeService = scope.ServiceProvider.GetRequiredService<DeleteExpiredEntityService>();
            await removeService.Process(_options.DaysBeforeDelete, cancellationToken);
            
            await Task.Delay(
                TimeSpan.FromHours(_options.WorkingCycleInHours),
                cancellationToken);
        }
    }
}