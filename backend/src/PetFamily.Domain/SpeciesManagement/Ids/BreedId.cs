using CSharpFunctionalExtensions;

namespace PetFamily.Domain.SpeciesManagement.Ids;

public class BreedId : ComparableValueObject
{
    private BreedId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; init; }

    public static BreedId Create(Guid id) => new BreedId(id);

    public static BreedId New() => new BreedId(Guid.NewGuid());
    
    public static BreedId Empty() => new(Guid.Empty);
    
    public static implicit operator BreedId(Guid id) => new(id);

    public static implicit operator Guid(BreedId breedId)
    {
        ArgumentNullException.ThrowIfNull(breedId);
        
        return breedId.Value;
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}