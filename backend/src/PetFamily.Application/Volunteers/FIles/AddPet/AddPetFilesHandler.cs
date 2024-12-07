using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.FIles.AddPet;

public class AddPetFilesHandler
{
    private readonly IFileProvider _fileProvider;

    public AddPetFilesHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<UnitResult<Error>> Handle(
        FileData fileData,
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        return null; //await _fileProvider.UploadFiles(fileData, cancellationToken);
    }
}