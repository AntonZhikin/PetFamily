using PetFamily.Disscusion.Application.DiscussionManagement.Commands.EditMessage;

namespace PetFamily.Disscusion.Presentation;

public record EditMessageRequest(Guid SenderId, Guid DiscussionId, Guid MessageId, string Text)
{
    public EditMessageCommand ToCommand() => new EditMessageCommand(SenderId, DiscussionId, MessageId, Text);
}