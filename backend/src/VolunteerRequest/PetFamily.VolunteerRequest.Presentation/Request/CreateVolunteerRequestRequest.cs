using PetFamily.Core.DTOs.Volunteer;
using PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.Create;

namespace PetFamily.VolunteerRequest.Presentation.Request;

public record CreateVolunteerRequestRequest(
    Guid UserId,
    FullNameDto FullName,
    VolunteerInfoDto VolunteerInfo,
    string Gender)
{
    public CreateVolunteerRequestCommand ToCommand() =>
        new(UserId,
            FullName,
            VolunteerInfo,
            Gender);
}