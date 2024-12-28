using CSharpFunctionalExtensions;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject;

namespace PetFamily.Core.File;

public interface IFileProvider
{
    Task<Result<IReadOnlyList<PhotoPath>, Error>> UploadFiles(
        IEnumerable<FileData> files,
        CancellationToken cancellationToken = default);
    
    Task<Result<string, Error>> GetFile(
        FileContent fileContent, string bucketName, 
        CancellationToken cancellationToken = default);
    
    Task<Result<bool, Error>> DeleteFile(
        FileInfo fileInfo, CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> RemoveFile(
        FileInfo fileInfo,
        CancellationToken cancellationToken = default);
}