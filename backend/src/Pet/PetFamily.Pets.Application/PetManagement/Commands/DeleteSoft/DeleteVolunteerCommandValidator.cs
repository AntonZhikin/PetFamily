using FluentValidation;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteSoft;

public class DeleteVolunteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerCommandValidator()
    {
        RuleFor(v => v.VolunteerId).NotEmpty();
    }
}