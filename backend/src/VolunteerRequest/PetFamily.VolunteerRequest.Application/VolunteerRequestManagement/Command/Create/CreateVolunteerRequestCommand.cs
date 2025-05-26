using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs.Volunteer;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.Create;

public record CreateVolunteerRequestCommand(
    Guid UserId, FullNameDto FullName, string PhoneNumber, VolunteerInfoDto VolunteerInfo) : ICommand;