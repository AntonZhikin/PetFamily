using PetFamily.Core.Abstractions;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Commands.EditMessage;

public record EditMessageCommand(Guid SenderId, Guid DiscussionId, Guid MessageId, string Text) : ICommand;