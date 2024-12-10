using PetFamily.Application.Volunteers.DTOs;
using PetFamily.Domain.Volunteer.VolunteerValueObject;

namespace PetFamily.Application.Volunteers.UpdateSocialNetworks;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    SocialNetworkListDto SocialNetworkList
    );