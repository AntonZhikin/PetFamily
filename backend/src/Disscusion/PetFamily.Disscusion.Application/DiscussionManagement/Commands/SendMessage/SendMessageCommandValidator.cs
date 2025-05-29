using FluentValidation;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Commands.SendMessage;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(command => command.Content).NotNull().NotEmpty();
    }
}