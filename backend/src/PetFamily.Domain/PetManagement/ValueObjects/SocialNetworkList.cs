namespace PetFamily.Domain.PetManagement.ValueObjects;

public record SocialNetworkList
{
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; }
    
    private SocialNetworkList() { }
    public SocialNetworkList(List<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }
    
    
}
