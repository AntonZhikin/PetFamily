using PetFamily.Disscusion.Domain.ValueObject;

namespace PetFamily.Disscusion.Domain.Entity;

public class Message
{
    private Message() { }

    public Message(Guid discussionId, Guid senderId, Content content)
    {
        CreatedAt = DateTime.UtcNow;
        Content = content;
        SenderId = senderId;
        DiscussionId = discussionId;
    }
    
    public Guid Id { get; private set; }
    
    public Guid SenderId { get; private set; }
    
    public Content Content { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public Guid DiscussionId { get; private set; }
    
    public bool IsEdited { get; private set; }
    public void EditMessage(Content newContent)
    {
        Content = newContent;
        IsEdited = true;
    }
}