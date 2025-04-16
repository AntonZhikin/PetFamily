namespace PetFamily.Disscusion.Domain.Entity;

public class Message
{
    private Message() { }

    public Message(Guid senderId, string text)
    {
        Id = Guid.NewGuid();
        SenderId = senderId;
        Text = text;
        CreatedAt = DateTime.UtcNow;
        IsEdited = false;
    }
    
    public Guid Id { get; private set; }
    
    public Guid SenderId { get; private set; }
    
    public string Text { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public bool IsEdited { get; private set; }
    public void EditMessage(string newText)
    {
        Text = newText;
        IsEdited = true;
    }
}