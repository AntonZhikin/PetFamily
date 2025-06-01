using CSharpFunctionalExtensions;
using FilesService.Core;
using FilesService.Core.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FilesService.MongoDataAccess;

public class FileRepository : IFileRepository
{
    private readonly FileMongoDbContext _dbContext;

    public FileRepository(FileMongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, Error>> Add(FileData fileData, CancellationToken cancellationToken)
    {
        await _dbContext.Files.InsertOneAsync(fileData, cancellationToken: cancellationToken);

        return fileData.Id;
    }

    public async Task<IReadOnlyCollection<FileData>> GetRange(IEnumerable<Guid> fileIds, CancellationToken cancellationToken)
    {
        return await _dbContext.Files.Find(f => fileIds.Contains(f.Id)).ToListAsync(cancellationToken);
    }
    
    
    public async Task<Result<FileData>> Get(Guid fileId, CancellationToken cancellationToken)
    {
        return await _dbContext.Files.AsQueryable()
            .FirstOrDefaultAsync(f => f.Id == fileId, cancellationToken: cancellationToken);
    }

    public async Task<UnitResult<Error>> DeleteMany(IEnumerable<Guid> fileIds, CancellationToken cancellationToken)
    {
        var deleteResult = await _dbContext.Files
            .DeleteManyAsync(f => fileIds.Contains(f.Id), cancellationToken: cancellationToken);
        
        if (deleteResult.DeletedCount == 0)
            return Errors.Files.FailRemove();

        return Result.Success<Error>();
    }
    
    public async Task<UnitResult<Error>> Remove(Guid id, CancellationToken ct)
    {
        var deleteResult = await _dbContext.Files
            .DeleteOneAsync(f => f.Id == id, cancellationToken: ct);
        
        return deleteResult.DeletedCount == 0
            ? Errors.Files.FailRemove()
            : Result.Success<Error>();
    }
}