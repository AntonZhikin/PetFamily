using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Providers;

public interface IFileProvider
{
    Task<Result<string, Error>> UploadFile(
        FileData fileData, CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetFile(
        FileData fileData, CancellationToken cancellationToken = default);
    Task<Result<bool, Error>> DeleteFile(
        FileData fileData, CancellationToken cancellationToken = default);
}