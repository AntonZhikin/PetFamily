using CSharpFunctionalExtensions;

namespace PetFamily.Kernel.ValueObject.Ids;

public class VolunteerId : ComparableValueObject
{
    private VolunteerId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static VolunteerId NewVolunteerId() => new(Guid.NewGuid());

    public static VolunteerId Empty() => new(Guid.Empty);
    
    public static VolunteerId Create(Guid id) => new(id);
    
    public static implicit operator VolunteerId(Guid id) => new(id);

    public static implicit operator Guid(VolunteerId volunteerId)
    {
        ArgumentNullException.ThrowIfNull(volunteerId);

        return volunteerId.Value;
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}