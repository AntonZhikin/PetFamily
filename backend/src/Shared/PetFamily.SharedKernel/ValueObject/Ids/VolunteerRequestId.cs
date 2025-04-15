using CSharpFunctionalExtensions;

namespace PetFamily.Kernel.ValueObject.Ids;

public class VolunteerRequestId : ComparableValueObject
{
    private VolunteerRequestId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static VolunteerRequestId NewVolunteerId() => new(Guid.NewGuid());

    public static VolunteerRequestId Empty() => new(Guid.Empty);
    
    public static VolunteerRequestId Create(Guid id) => new(id);
    
    public static implicit operator VolunteerRequestId(Guid id) => new(id);

    public static implicit operator Guid(VolunteerRequestId volunteerRequestId)
    {
        ArgumentNullException.ThrowIfNull(volunteerRequestId);

        return volunteerRequestId.Value;
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}