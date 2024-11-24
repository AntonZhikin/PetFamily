using PetFamily.Application.Volunteers.DTOs;
using PetFamily.Domain.Volunteer.VolunteerValueObject;

namespace PetFamily.Application.Volunteers.UpdateSocialNetworks;

public record UpdateSocialNetworkRequest(
    Guid VolunteerId,
    UpdateSocialNetworksDto SocialNetworkDto
    );
public record UpdateSocialNetworksDto(
    List<SocialNetworkDto> SocialNetworks);
    