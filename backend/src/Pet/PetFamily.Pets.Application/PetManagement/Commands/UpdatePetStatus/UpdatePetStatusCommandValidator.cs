using FluentValidation;
using PetFamily.Core;
using PetFamily.Core.Validation;
using PetFamily.Kernel;

namespace PetFamily.Pets.Application.PetManagement.Commands.UpdatePetStatus;

public class UpdatePetStatusCommandValidator : AbstractValidator<UpdatePetStatusCommand>
{
    public UpdatePetStatusCommandValidator()
    {
        RuleFor(command => command.PetId).NotNull().WithError(Errors.General.ValueIsRequired());
        RuleFor(command => command.VolunteerId).NotNull().WithError(Errors.General.ValueIsRequired());
        RuleFor(command => command.NewStatus).NotNull().WithError(Errors.General.ValueIsRequired());
    }
}