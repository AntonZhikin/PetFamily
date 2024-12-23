using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.PetManagement.Commands.DeleteSoftPet;

public class DeleteSoftPetCommandValidator : AbstractValidator<DeleteSoftPetCommand>
{
    public DeleteSoftPetCommandValidator()
    {
        RuleFor(command => command.PetId).NotNull().WithError(Errors.General.ValueIsRequired());
        RuleFor(command => command.VolunteerId).NotNull().WithError(Errors.General.ValueIsRequired());
    }
}