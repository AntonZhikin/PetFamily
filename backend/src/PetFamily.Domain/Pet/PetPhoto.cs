using System.Reflection.Metadata;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet;

public record PetPhoto //: Entity<PetPhotoId>
{
    public PetPhoto(PhotoPath path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }
    public PhotoPath Path { get; }
    
    public bool IsMain { get; }
    
}