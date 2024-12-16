using Microsoft.Extensions.Logging;
using PetFamily.Application.Files;
using PetFamily.Application.Messaging;
using FileInfo = PetFamily.Application.Files.FileInfo;

namespace PetFamily.Infrastructure.BackgroundServices;

public class FilesCleanerService : IFilesCleanerService
{
    private readonly ILogger<FilesCleanerService> _logger;
    private readonly IFileProvider _fileProvider;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    
    public FilesCleanerService(
        IFileProvider fileProvider, 
        ILogger<FilesCleanerService> logger, 
        IMessageQueue<IEnumerable<FileInfo>> messageQueue)
    {
        _fileProvider = fileProvider;
        _logger = logger;
        _messageQueue = messageQueue;
    }

    public async Task Process(CancellationToken stoppingToken)
    {
        var fileInfos = await _messageQueue.ReadAsync(stoppingToken);

        foreach (var fileInfo in fileInfos)
        {
            await _fileProvider.RemoveFile(fileInfo, stoppingToken);
        }
    }
}