using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.FIles.DeletePet;

public class DeletePetFilesHandler
{
    private readonly IFileProvider _fileProvider;

    public DeletePetFilesHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<bool, Error>> Handle(
        FileContent fileContent,
        string bucketName,
        CancellationToken cancellationToken)
    {
        //return await _fileProvider.DeleteFile(fileContent, bucketName, cancellationToken);
        return null;
    }
}