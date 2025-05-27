using PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetReopenStatus;

namespace PetFamily.VolunteerRequest.Presentation.Request;

public record SetReopenStatusRequest(string Comment)
{
    public SetReopenStatusCommand ToCommand(Guid userId, Guid requestId) =>
        new(userId, requestId, Comment);
}