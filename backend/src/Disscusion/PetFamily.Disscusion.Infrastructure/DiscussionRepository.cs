using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Disscusion.Application;
using PetFamily.Disscusion.Infrastructure.DbContext;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Infrastructure;


public class DiscussionsRepository : IDiscussionsRepository
{
    private readonly DiscussionsWriteDbContext _dbContext;

    public DiscussionsRepository(DiscussionsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.AggregateRoot.Discussion> Add(
        Domain.AggregateRoot.Discussion discussion, CancellationToken cancellationToken)
    {
        _dbContext.Discussions.Add(discussion);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return discussion;
    }

    public async Task<Result<Domain.AggregateRoot.Discussion, Error>> GetByParticipantsId(
        Guid applicantUserId, Guid reviewingUserId,
        CancellationToken cancellationToken)
    {
        var discussion = await _dbContext.Discussions
            .Include(d => d.Messages)
            .FirstOrDefaultAsync(d => d.DiscussionUsers.ReviewingUserId == reviewingUserId &&
                                      d.DiscussionUsers.ApplicantUserId == applicantUserId, cancellationToken);

        if (discussion == null)
            return Errors.General.NotFound();

        return discussion;
    }

    public async Task<Result<Domain.AggregateRoot.Discussion, Error>> GetById(
        Guid discussionId,
        CancellationToken cancellationToken)
    {
        var discussion = await _dbContext.Discussions
            .Include(d => d.Messages)
            .FirstOrDefaultAsync(d => d.Id == discussionId, cancellationToken);

        if (discussion == null)
            return Errors.General.NotFound(discussionId);

        return discussion;
    }

    public async Task<Result<Domain.AggregateRoot.Discussion, Error>> GetByRequestId(
        Guid requestId,
        CancellationToken cancellationToken)
    {
        var discussion = await _dbContext.Discussions
            .Include(d => d.Messages)
            .FirstOrDefaultAsync(d => d.RequestId == requestId, cancellationToken);

        if (discussion == null)
            return Errors.General.NotFound(requestId);

        return discussion;
    }
}