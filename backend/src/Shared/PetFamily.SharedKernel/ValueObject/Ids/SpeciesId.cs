using CSharpFunctionalExtensions;

namespace PetFamily.Kernel.ValueObject.Ids;

public class SpeciesId : ComparableValueObject
{
    private SpeciesId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static SpeciesId NewSpeciesId() => new(Guid.NewGuid());
    
    public static SpeciesId Empty() => new(Guid.Empty);
    
    public static SpeciesId Create(Guid id) => new(id);
    
    public static implicit operator SpeciesId(Guid id) => new(id);
    public static implicit operator Guid(SpeciesId speciesId)
    {
        ArgumentNullException.ThrowIfNull(speciesId);

        return speciesId.Value;
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}