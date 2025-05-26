using FluentValidation;

namespace PetFamily.Accounts.Application.AccountManagement.Commands.RegisterVolunteer;

public class RegisterVolunteerCommandValidator : AbstractValidator<RegisterVolunteerCommand>
{
    public RegisterVolunteerCommandValidator()
    {
        RuleFor(c => c.UserId)
            .Must(id => id != Guid.Empty).WithMessage("UserId cannot be empty");
    }
}