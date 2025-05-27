using PetFamily.Core.DTOs.Volunteer;
using PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.Create;

namespace PetFamily.VolunteerRequest.Presentation.Request;

public record CreateVolunteerRequestRequest(
    Guid UserId,
    FullNameDto FullName,
    string PhoneNumber,
    VolunteerInfoDto VolunteerInfo)
{
    public CreateVolunteerRequestCommand ToCommand() =>
        new(UserId,
            FullName,
            PhoneNumber,
            VolunteerInfo);
}