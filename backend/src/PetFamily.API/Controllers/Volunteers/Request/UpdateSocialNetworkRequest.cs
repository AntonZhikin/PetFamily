using PetFamily.Application.DTOs;
using PetFamily.Application.PetManagement.Commands.UpdateSocialNetworks;

namespace PetFamily.API.Controllers.Volunteers.Request;

public record UpdateSocialNetworkRequest(SocialNetworkListDto SocialNetworkList)
{
    public UpdateSocialNetworkCommand ToCommand(Guid id) => new(id, SocialNetworkList);
}
