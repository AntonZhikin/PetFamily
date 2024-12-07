namespace PetFamily.Domain.Speciess.SpeciesID;

public record BreedId
{
    private BreedId()
    {
    }
    
    private BreedId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static BreedId NewPetId() => new(Guid.NewGuid());

    public static BreedId Empty() => new(Guid.Empty);
}