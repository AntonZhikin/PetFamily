using PetFamily.Core.DTOs.ValueObject;
using PetFamily.Pets.Application.PetManagement.Commands.UpdateSocialNetworks;

namespace PetFamily.Pets.Controllers.Volunteers.Request;

public record UpdateSocialNetworkRequest(SocialNetworkListDto SocialNetworkList)
{
    public UpdateSocialNetworkCommand ToCommand(Guid id) => new(id, SocialNetworkList);
}
