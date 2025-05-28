using FluentValidation;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Commands.Create;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(c => c.RelationId).NotEqual(Guid.Empty);
    }
}