using FluentValidation;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Commands.Close;

public class CloseCommandValidator : AbstractValidator<CloseCommand>
{
    public CloseCommandValidator()
    {
        RuleFor(c => c.DiscussiondId).NotEqual(Guid.Empty);
    }
}