namespace PetFamily.Core.DTOs.Discussion;

public class MessageDto
{
    public Guid Id { get; init; }
    public string Content { get; init; } = string.Empty;
    public Guid SenderId { get; init; }
    public Guid DiscussionId { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool IsEdited { get; init; }
}