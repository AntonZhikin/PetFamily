using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer;

public class SocialMedia : Entity
{
    private SocialMedia(Guid id) : base(id)
    {
        Id = id;
    }
    
    public Guid Id { get; private set; }
    
    public string Path { get; private set; } = null!;

    public string Name { get; private set; } = null!;
}