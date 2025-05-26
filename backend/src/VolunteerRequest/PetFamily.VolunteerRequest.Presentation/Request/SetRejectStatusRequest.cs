using PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetRejectStatus;

namespace PetFamily.VolunteerRequest.Presentation.Request;

public record SetRejectStatusRequest(string Comment, bool IsBanNeed = false)
{
    public SetRejectStatusCommand ToCommand(Guid adminId, Guid requestId) =>
        new(adminId, requestId, Comment, IsBanNeed);
}