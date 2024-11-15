using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer;

public record SocialNetwork 
{
    public string Link { get; } = null!;

    public string Name { get; } = null!;

    private SocialNetwork(string link, string name)
    {
        Name = name;
        Link = link;
    }
    
    public static Result<SocialNetwork, Error> Create(string name, string link)
    {
        var newSocialNetwork = SocialNetwork.Create(name, link);
        
        return newSocialNetwork;
    }
}