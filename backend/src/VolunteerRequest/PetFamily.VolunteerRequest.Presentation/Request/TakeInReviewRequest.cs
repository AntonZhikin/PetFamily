using PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.TakeInReview;

namespace PetFamily.VolunteerRequest.Presentation.Request;

public record TakeInReviewRequest(Guid RequestId)
{
    public TakeInReviewCommand ToCommand(Guid adminId) =>
        new(adminId, RequestId);
}