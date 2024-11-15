namespace PetFamily.Domain.Volunteer.VolunteerID;

public class SocialMediaId
{
    private SocialMediaId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static SocialMediaId NewPetId() => new(Guid.NewGuid());

    public static SocialMediaId Empty() => new(Guid.Empty);
}