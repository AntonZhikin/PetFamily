using PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetApprovedStatus;

namespace PetFamily.VolunteerRequest.Presentation.Request;

public record SetApprovedStatusRequest(string Comment)
{
    public SetApprovedStatusCommand ToCommand(Guid adminId, Guid requestId) =>
        new(adminId, requestId, Comment);
}