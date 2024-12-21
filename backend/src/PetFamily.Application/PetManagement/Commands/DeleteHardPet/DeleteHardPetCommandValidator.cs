using FluentValidation;

namespace PetFamily.Application.PetManagement.Commands.DeleteHardPet;

public class DeleteHardPetCommandValidator : AbstractValidator<DeleteHardPetCommand>
{
    public DeleteHardPetCommandValidator()
    {
        RuleFor(command => command.PetId).NotEmpty();
        RuleFor(command => command.VolunteerId).NotEmpty();
    }
}