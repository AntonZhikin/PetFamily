 namespace PetFamily.Pets.Application.Files;

public interface IFilesCleanerService
{
    Task Process(CancellationToken stoppingToken);
}