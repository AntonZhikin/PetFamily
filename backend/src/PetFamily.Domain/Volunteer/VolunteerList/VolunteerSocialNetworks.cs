namespace PetFamily.Domain.Volunteer.VolunteerList;

public record VolunteerSocialNetworks
{
    private VolunteerSocialNetworks() { }
    public VolunteerSocialNetworks(IReadOnlyList<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }
    
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; }
}
