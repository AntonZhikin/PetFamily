using FluentValidation;

namespace PetFamily.Application.Volunteers.DeleteHard;

public class DeleteVolunteerHardCommandValidator : AbstractValidator<DeleteVolunteerHardCommand>
{
    public DeleteVolunteerHardCommandValidator()
    {
        RuleFor(v => v.VolunteerId).NotEmpty();
    }
}