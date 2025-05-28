using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Application;


public interface IDiscussionsRepository
{
    public Task<Domain.AggregateRoot.Discussion> Add(Domain.AggregateRoot.Discussion discussion, CancellationToken cancellationToken);

    public Task<Result<Domain.AggregateRoot.Discussion, Error>> GetByParticipantsId(
        Guid ApplicantUserId, Guid ReviewingUserId,
        CancellationToken cancellationToken);

    public Task<Result<Domain.AggregateRoot.Discussion, Error>> GetById(
        Guid discussionId,
        CancellationToken cancellationToken);

    public Task<Result<Domain.AggregateRoot.Discussion, Error>> GetByRequestId(
        Guid requestId,
        CancellationToken cancellationToken);
}