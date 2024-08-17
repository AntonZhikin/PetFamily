using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer;

public class SocialMedia : Entity<SocialMediaId>
{
    private SocialMedia(SocialMediaId socialMediaId, string path, string name) : base(socialMediaId)
    {
        Path = path;
        Name = name;
    }
    
    //public Guid Id { get; private set; }
    
    public string Path { get; private set; } = null!;

    public string Name { get; private set; } = null!;
}