using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetManagement.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.Volunteers.UpdateAssistanceDetail;

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