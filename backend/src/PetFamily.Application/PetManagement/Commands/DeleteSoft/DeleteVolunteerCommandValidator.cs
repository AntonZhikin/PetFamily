using FluentValidation;

namespace PetFamily.Application.PetManagement.Commands.DeleteSoft;

public class DeleteVolunteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerCommandValidator()
    {
        RuleFor(v => v.VolunteerId).NotEmpty();
    }
}