using PetFamily.Application.Abstractions;
using PetFamily.Application.DTOs;
using PetFamily.Application.DTOs.ValueObject;

namespace PetFamily.Application.PetManagement.Commands.UpdateSocialNetworks;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    SocialNetworkListDto SocialNetworkList
    ) : ICommand;