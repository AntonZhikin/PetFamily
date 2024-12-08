using FluentValidation;

namespace PetFamily.Application.Volunteers.DeleteFilesToPet;

public class DeleteFilesToPetCommandValidator : AbstractValidator<DeleteFilesToPetCommand>
{
    public DeleteFilesToPetCommandValidator()
    {
        //RuleFor(x => x.VolunteerId).NotEmpty();
        //RuleFor(x => x.PetId).NotEmpty();
    }
}