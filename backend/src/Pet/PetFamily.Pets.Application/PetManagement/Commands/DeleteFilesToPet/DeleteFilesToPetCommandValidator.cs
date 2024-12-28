using FluentValidation;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteFilesToPet;

public class DeleteFilesToPetCommandValidator : AbstractValidator<DeleteFilesToPetCommand>
{
    public DeleteFilesToPetCommandValidator()
    {
        //RuleFor(x => x.VolunteerId).NotEmpty();
        //RuleFor(x => x.PetId).NotEmpty();
    }
}