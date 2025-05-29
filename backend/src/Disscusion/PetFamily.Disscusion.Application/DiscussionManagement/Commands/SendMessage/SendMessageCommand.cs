using PetFamily.Core.Abstractions;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Commands.SendMessage;

public record SendMessageCommand(Guid DiscussionId, Guid UserId, string Content) : ICommand;