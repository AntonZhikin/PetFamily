using FluentValidation;
using PetFamily.Core;
using PetFamily.Core.Validation;
using PetFamily.Kernel;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteSoftPet;

public class DeleteSoftPetCommandValidator : AbstractValidator<DeleteSoftPetCommand>
{
    public DeleteSoftPetCommandValidator()
    {
        RuleFor(command => command.PetId).NotNull().WithError(Errors.General.ValueIsRequired());
        RuleFor(command => command.VolunteerId).NotNull().WithError(Errors.General.ValueIsRequired());
    }
}