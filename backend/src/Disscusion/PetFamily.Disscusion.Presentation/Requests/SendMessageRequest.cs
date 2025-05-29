using PetFamily.Disscusion.Application.DiscussionManagement.Commands.SendMessage;

namespace PetFamily.Disscusion.Presentation;

public record SendMessageRequest(Guid DiscussionId, Guid UserId, string Content)
{
    public SendMessageCommand ToCommand() => new SendMessageCommand(DiscussionId, UserId, Content);
}