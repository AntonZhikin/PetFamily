using CSharpFunctionalExtensions;
using FilesService.Core;
using FilesService.Core.Models;

namespace FilesService.MongoDataAccess;

public interface IFileRepository
{
    Task<Result<Guid, Error>> Add(FileData fileData, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<FileData>> GetRange(IEnumerable<Guid> fileIds, CancellationToken cancellationToken);
    Task<Result<FileData>> Get(Guid fileId, CancellationToken cancellationToken);
    Task<UnitResult<Error>> DeleteMany(IEnumerable<Guid> fileIds, CancellationToken cancellationToken);
    Task<UnitResult<Error>> Remove(Guid id, CancellationToken cancellationToken);
}