namespace PetFamily.Pets.Domain.ValueObjects;

public record SocialNetworkList
{
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; }
    
    private SocialNetworkList() { }
    public SocialNetworkList(List<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }
    
    
}
