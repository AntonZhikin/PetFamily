namespace PetFamily.Disscusion.Contracts.Request;

public record CreateDiscussionRequest(Guid RequestId, Guid ReviewingUserId, Guid ApplicantUserId);