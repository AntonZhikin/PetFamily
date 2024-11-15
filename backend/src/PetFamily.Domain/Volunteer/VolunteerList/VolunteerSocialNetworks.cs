using PetFamily.Domain.Volunteer.VolunteerValueObject;

namespace PetFamily.Domain.Volunteer.VolunteerList;

public record VolunteerSocialNetworks
{
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; }
    
    private VolunteerSocialNetworks() { }
    public VolunteerSocialNetworks(List<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }
    
    
}
