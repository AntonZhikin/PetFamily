using PetFamily.Application.Abstractions;
using PetFamily.Application.DTOs;

namespace PetFamily.Application.PetManagement.Commands.UpdateSocialNetworks;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    SocialNetworkListDto SocialNetworkList
    ) : ICommand;