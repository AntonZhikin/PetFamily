using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;
using IFileProvider = PetFamily.Application.FileProvider.IFileProvider;

namespace PetFamily.Application.Volunteers.FIles.GetPet;

public class GetPetFilesHandler
{
    private readonly IFileProvider _fileProvider;

    public GetPetFilesHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> Handle(
        FileContent fileContent,
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        return await _fileProvider.GetFile(fileContent, bucketName, cancellationToken);
    }
}