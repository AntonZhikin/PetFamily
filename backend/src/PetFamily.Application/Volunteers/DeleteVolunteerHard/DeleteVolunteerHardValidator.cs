using FluentValidation;

namespace PetFamily.Application.Volunteers.DeleteVolunteerHard;

public class DeleteVolunteerHardValidator : AbstractValidator<DeleteVolunteerHardRequest>
{
    public DeleteVolunteerHardValidator()
    {
        RuleFor(v => v.VolunteerId).NotEmpty();
    }
}