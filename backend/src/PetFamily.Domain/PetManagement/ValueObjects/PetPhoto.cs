namespace PetFamily.Domain.PetManagement.ValueObjects;

public record PetPhoto //: Entity<PetPhotoId>
{
    public PetPhoto(PhotoPath path, bool isMain = false)
    {
        Path = path;
        IsMain = isMain;
    }
    public PhotoPath Path { get; }
    
    public bool IsMain { get; }
}