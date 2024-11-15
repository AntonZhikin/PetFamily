namespace PetFamily.Domain.Volunteer.VolunteerID;

public class RequisiteForHelpId
{
    private RequisiteForHelpId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static RequisiteForHelpId NewPetId() => new(Guid.NewGuid());

    public static RequisiteForHelpId Empty() => new(Guid.Empty);
}