using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.PetManagement.Commands.UpdatePetStatus;

public class UpdatePetStatusCommandValidator : AbstractValidator<UpdatePetStatusCommand>
{
    public UpdatePetStatusCommandValidator()
    {
        RuleFor(command => command.PetId).NotNull().WithError(Errors.General.ValueIsRequired());
        RuleFor(command => command.VolunteerId).NotNull().WithError(Errors.General.ValueIsRequired());
        RuleFor(command => command.NewStatus).NotNull().WithError(Errors.General.ValueIsRequired());
    }
}