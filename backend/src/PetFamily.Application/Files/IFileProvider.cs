using CSharpFunctionalExtensions;
using PetFamily.Domain.PetManagement.ValueObjects;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.Files;

public interface IFileProvider
{
    // Task<UnitResult<Error>> UploadFiles(
    //     FileData fileData, 
    //     CancellationToken cancellationToken = default);
    //
    Task<Result<IReadOnlyList<PhotoPath>, Error>> UploadFiles(
        IEnumerable<FileData> files,
        CancellationToken cancellationToken = default);
    
    Task<Result<string, Error>> GetFile(
        FileContent fileContent, string bucketName, 
        CancellationToken cancellationToken = default);
    
    Task<Result<bool, Error>> DeleteFile(
        FileData fileData, CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> RemoveFile(
        FileInfo fileInfo,
        CancellationToken cancellationToken = default);
}