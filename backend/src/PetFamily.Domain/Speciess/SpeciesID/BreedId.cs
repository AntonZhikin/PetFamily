namespace PetFamily.Domain.Speciess;

public record BreedId
{
    private BreedId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static BreedId NewPetId() => new(Guid.NewGuid());

    public static BreedId Empty() => new(Guid.Empty);
}