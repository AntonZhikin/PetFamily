namespace PetFamily.Domain.Pet;

public record PetPhotoId
{
    private PetPhotoId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static PetPhotoId NewPetId() => new(Guid.NewGuid());

    public static PetPhotoId Empty() => new(Guid.Empty);
}