using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
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
        FileData fileData,
        CancellationToken cancellationToken)
    {
        return await _fileProvider.DeleteFile(fileData, cancellationToken);
    }
}