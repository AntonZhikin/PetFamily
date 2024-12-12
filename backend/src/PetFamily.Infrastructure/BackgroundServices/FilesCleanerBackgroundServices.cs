using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using PetFamily.Application.Files;

namespace PetFamily.Infrastructure.BackgroundServices;

public class FilesCleanerBackgroundServices : BackgroundService
{
    private readonly ILogger<FilesCleanerBackgroundServices> _logger;

    private readonly IServiceScopeFactory _scopeFactory;

    public FilesCleanerBackgroundServices(
        ILogger<FilesCleanerBackgroundServices> logger, 
        IServiceScopeFactory serviceProvider)
    {
        _logger = logger;
        _scopeFactory = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("FilesCleanerBackgroundServices start");
        
        await using var scope = _scopeFactory.CreateAsyncScope();
        
        var filesCleanerServices = scope.ServiceProvider.GetRequiredService<IFilesCleanerService>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await filesCleanerServices.Process(stoppingToken);
        }

        await Task.CompletedTask;
    }
}