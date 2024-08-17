namespace PetFamily.Domain.Pet;

public record RequisiteId
{
    private RequisiteId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static RequisiteId NewPetId() => new(Guid.NewGuid());

    public static RequisiteId Empty() => new(Guid.Empty);
}
