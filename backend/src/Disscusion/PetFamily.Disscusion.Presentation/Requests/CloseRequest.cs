using PetFamily.Disscusion.Application.DiscussionManagement.Commands.Close;

namespace PetFamily.Disscusion.Presentation;

public record CloseRequest(Guid DiscussionId)
{
    public CloseCommand ToCommand() => new CloseCommand(DiscussionId);
}