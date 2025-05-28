using PetFamily.Disscusion.Application.DiscussionManagement.Commands.DeleteMessage;

namespace PetFamily.Disscusion.Presentation;

public record DeleteMessageRequest(Guid MessageId, Guid UserId, Guid DiscussionId)
{
    public DeleteMessageCommand ToCommand(Guid userId, Guid discussionId) =>
        new(userId, discussionId, MessageId);
}