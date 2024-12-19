using FluentValidation;

namespace PetFamily.Application.PetManagement.Commands.DeleteHard;

public class DeleteVolunteerHardCommandValidator : AbstractValidator<DeleteVolunteerHardCommand>
{
    public DeleteVolunteerHardCommandValidator()
    {
        RuleFor(v => v.VolunteerId).NotEmpty();
    }
}