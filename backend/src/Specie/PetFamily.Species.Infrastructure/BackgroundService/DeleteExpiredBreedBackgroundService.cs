using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Kernel;
using PetFamily.Species.Infrastructure.Services;

namespace PetFamily.Species.Infrastructure.BackgroundService;

public class DeleteExpiredBreedBackgroundService : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly ILogger<DeleteExpiredBreedBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public DeleteExpiredBreedBackgroundService(
        ILogger<DeleteExpiredBreedBackgroundService> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("DeleteBreedBackgroundService is started");

        while (!cancellationToken.IsCancellationRequested)
        {
            await using var scope = _scopeFactory.CreateAsyncScope();

            var deleteExpiredSpeciesService = scope.ServiceProvider
                .GetRequiredService<DeleteExpiredBreedService>();

            _logger.LogInformation("DeleteExpiredBreedBackgroundService is working");

            await deleteExpiredSpeciesService.Process(cancellationToken);

            await Task.Delay(
                TimeSpan.FromHours(Constants.DELETE_EXPIRED_BREED_SERVICE_REDUCTION_HOURS),
                cancellationToken);
        }
    }
}