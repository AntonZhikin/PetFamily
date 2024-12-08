using FluentValidation;

namespace PetFamily.Application.Volunteers.DeleteSoft;

public class DeleteVolunteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerCommandValidator()
    {
        RuleFor(v => v.VolunteerId).NotEmpty();
    }
}