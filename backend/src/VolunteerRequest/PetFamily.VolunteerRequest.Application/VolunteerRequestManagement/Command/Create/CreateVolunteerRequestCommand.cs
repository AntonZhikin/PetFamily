using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs.Volunteer;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.Create;

public record CreateVolunteerRequestCommand(
    Guid UserId, FullNameDto FullName, VolunteerInfoDto PhoneNumber, string VolunteerInfo) : ICommand;