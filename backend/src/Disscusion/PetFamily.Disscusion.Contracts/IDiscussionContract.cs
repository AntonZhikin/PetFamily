using CSharpFunctionalExtensions;
using PetFamily.Disscusion.Contracts.Request;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Contracts;

public interface IDiscussionContract
{
    Task<Result<Guid, ErrorList>> CreateDiscussionForVolunteerRequest(CreateDiscussionRequest request,
        CancellationToken cancellationToken = default);
}