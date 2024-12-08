using CSharpFunctionalExtensions;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.FileProvider;

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
}