using CSharpFunctionalExtensions;
using PetFamily.Disscusion.Domain.Entity;
using PetFamily.Disscusion.Domain.ValueObject;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Domain.AggregateRoot;

public class Discussion
{
    // ef core
    private Discussion() { }

    public Discussion(
        Guid requestId,
        DiscussionUsers discussionUsers)
    {
        RequestId = requestId;
        DiscussionUsers = discussionUsers;
        Status = DiscussionStatus.Open;
    }

    public Guid Id { get; private set; }
    
    public Guid RequestId { get; private set; }

    public DiscussionUsers DiscussionUsers { get; } = default!;
    
    private List<Message> _messages = [];
    
    public IReadOnlyList<Message> Messages => _messages;
    
    public DiscussionStatus Status { get; private set; }
    
    // Methods

    public static Result<Discussion, Error> Create(
        Guid requestId,
        DiscussionUsers discussionUsers)
    {
        var discussion = new Discussion(requestId, discussionUsers)
        {
            Status = DiscussionStatus.Open
        };
        return discussion;
    }
    public void Close()
    {
        Status = DiscussionStatus.Closed;
    }

    public UnitResult<Error> AddMessage(Message message)
    {
        if (Status == DiscussionStatus.Closed)
            return Errors.Discussion.ClosedDiscussion();

        if (IsFromUserInDiscussion(message) == false)
            return Errors.Discussion.UserNotInDiscussion();

        _messages.Add(message);
        return Result.Success<Error>();
    }
    
    public UnitResult<Error> RemoveMessage(Guid messageId, Guid userId)
    {
        if (Status == DiscussionStatus.Closed)
            return Errors.Discussion.ClosedDiscussion();

        var message = _messages.FirstOrDefault(m => m.Id == messageId);
        if (message is null)
            return Errors.General.NotFound();
        
        if (message.SenderId != userId)
            return Errors.Discussion.UserNotInDiscussion();
        
        _messages.Remove(message);
        return Result.Success<Error>();
    }
    
    public UnitResult<Error> EditMessage(
        Guid messageId, Guid userId, Content content)
    {
        if (Status == DiscussionStatus.Closed)
            return Errors.Discussion.ClosedDiscussion();

        var message = _messages.FirstOrDefault(m => m.Id == messageId);
        if (message is null)
            return Errors.General.NotFound();
        
        if (message.SenderId != userId)
            return Errors.Discussion.UserNotInDiscussion();
        
        message.EditMessage(content);
        
        return Result.Success<Error>();
    }
    
    private bool IsFromUserInDiscussion(Message message)
    {
        return DiscussionUsers.ApplicantUserId == message.SenderId ||
               DiscussionUsers.ReviewingUserId == message.SenderId;
    }
    
}