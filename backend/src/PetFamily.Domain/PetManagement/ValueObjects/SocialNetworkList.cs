using PetFamily.Domain.Volunteer.VolunteerValueObject;

namespace PetFamily.Domain.Volunteer.VolunteerList;

public record SocialNetworkList
{
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; }
    
    private SocialNetworkList() { }
    public SocialNetworkList(List<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }
    
    
}
