using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs.ValueObject;

namespace PetFamily.Pets.Application.PetManagement.Commands.UpdateSocialNetworks;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    SocialNetworkListDto SocialNetworkList
    ) : ICommand;