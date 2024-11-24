using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet;

public record PetPhoto //: Entity<PetPhotoId>
{
    //ef core
    //private PetPhoto(PetPhotoId id) : base(id)
    //{
    //}
    private PetPhoto(string path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }
    
    
    public string Path { get; }
    
    public bool IsMain { get; }

    public static Result<PetPhoto, Error> Create(string path, bool isMain = false)
    {
        if (string.IsNullOrWhiteSpace(path))
            return Errors.General.ValueIsRequired("FilePath");

        if (path.Length > Constants.MAX_HIGH_TEXT_LENGHT)
            return Errors.General.ValueIsRequired("FilePath");

        return new PetPhoto(path, isMain);
    }
}