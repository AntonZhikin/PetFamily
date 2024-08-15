namespace PetFamily.Domain.Volunteer;

public class SocialMedia
{
    public Guid Id { get; private set; }
    
    public string Path { get; private set; } = null!;

    public string Name { get; private set; } = null!;
}