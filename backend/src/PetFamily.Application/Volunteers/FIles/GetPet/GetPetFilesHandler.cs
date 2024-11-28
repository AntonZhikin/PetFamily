using CSharpFunctionalExtensions;
using Microsoft.Extensions.FileProviders;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;
using IFileProvider = PetFamily.Application.Providers.IFileProvider;

namespace PetFamily.Application.Volunteers.FIles.GetPetFiles;

public class GetPetFilesHandler
{
    private readonly IFileProvider _fileProvider;

    public GetPetFilesHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> Handle(
        FileData fileData,
        CancellationToken cancellationToken = default)
    {
        return await _fileProvider.GetFile(fileData, cancellationToken);
    }
}