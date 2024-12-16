using CSharpFunctionalExtensions;
using PetFamily.Application.Files;
using PetFamily.Domain.Shared.Error;
using IFileProvider = PetFamily.Application.Files.IFileProvider;

namespace PetFamily.Application.PetManagement.Commands.FIles.GetPet;

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