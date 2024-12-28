using FluentValidation;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteHard;

public class DeleteVolunteerHardCommandValidator : AbstractValidator<DeleteVolunteerHardCommand>
{
    public DeleteVolunteerHardCommandValidator()
    {
        RuleFor(v => v.VolunteerId).NotEmpty();
    }
}