using PetFamily.Core.Abstractions;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Commands.DeleteMessage;

public record DeleteMessageCommand(
    Guid SenderId, Guid DiscussionId, Guid MessageId) : ICommand;