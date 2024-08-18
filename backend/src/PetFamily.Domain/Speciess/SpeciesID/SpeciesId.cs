namespace PetFamily.Domain.Speciess;

public record SpeciesId
{
    private SpeciesId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static SpeciesId NewPetId() => new(Guid.NewGuid());

    public static SpeciesId Empty() => new(Guid.Empty);
}