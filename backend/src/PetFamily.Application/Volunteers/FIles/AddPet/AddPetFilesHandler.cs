using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.AddPet;

public class AddPetFilesHandler
{
    private readonly IFileProvider _fileProvider;

    public AddPetFilesHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> Handle(
        FileData fileData,
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        return await _fileProvider.UploadFile(fileData, bucketName, cancellationToken);
    }
}