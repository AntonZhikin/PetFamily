using FluentValidation;
using PetFamily.Core;
using PetFamily.Core.Validation;
using PetFamily.Kernel;
using PetFamily.Pets.Domain.ValueObjects;

namespace PetFamily.Pets.Application.PetManagement.Commands.UpdateAssistanceDetail;

public class UpdateAssistanceDetailCommandValidator : AbstractValidator<UpdateAssistanceDetailCommand>
{
    public UpdateAssistanceDetailCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());

        RuleForEach(u => u.AssistanceDetailList.AssistanceDetails)
            .MustBeValueObject(ua => AssistanceDetail.Create(
                ua.Name,
                ua.Description));
    }
}