using PetFamily.Application.Volunteers.DTOs;
using PetFamily.Application.Volunteers.UpdateSocialNetworks;

namespace PetFamily.API.Controllers.Volunteers.Request;

public record UpdateSocialNetworkRequest(SocialNetworkListDto SocialNetworkList)
{
    public UpdateSocialNetworkCommand ToCommand(Guid id) => new(id, SocialNetworkList);
}
