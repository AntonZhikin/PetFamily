using FluentValidation;

namespace PetFamily.Application.Volunteers.DeleteVolunteer;

public class DeleteVolunteerValidator : AbstractValidator<DeleteVolunteerRequest>
{
    public DeleteVolunteerValidator()
    {
        RuleFor(v => v.VolunteerId).NotEmpty();
    }
}