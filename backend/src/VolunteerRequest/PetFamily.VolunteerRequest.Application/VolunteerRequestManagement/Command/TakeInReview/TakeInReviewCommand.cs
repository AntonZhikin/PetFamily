using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.TakeInReview;

public record TakeInReviewCommand(Guid AdminId, Guid RequestId) : ICommand;