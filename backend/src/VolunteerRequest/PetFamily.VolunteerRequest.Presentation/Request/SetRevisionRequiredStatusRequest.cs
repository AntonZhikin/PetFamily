using PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetRevisionRequiredStatus;

namespace PetFamily.VolunteerRequest.Presentation.Request;

public record SetRevisionRequiredStatusRequest(string Comment)
{
    public SetRevisionRequiredStatusCommand ToCommand(Guid adminId, Guid requestId) =>
        new(adminId, requestId, Comment);
}