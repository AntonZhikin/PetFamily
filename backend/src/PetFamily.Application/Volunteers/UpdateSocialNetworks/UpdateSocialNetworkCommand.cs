using PetFamily.Application.Volunteers.DTOs;

namespace PetFamily.Application.Volunteers.UpdateSocialNetworks;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    SocialNetworkListDto SocialNetworkList
    );