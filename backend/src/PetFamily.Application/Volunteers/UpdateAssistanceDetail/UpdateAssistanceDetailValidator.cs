using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer.VolunteerValueObject;

namespace PetFamily.Application.Volunteers.UpdateAssistanceDetail;

public class UpdateAssistanceDetailValidator : AbstractValidator<UpdateAssistanceDetailRequest>
{
    public UpdateAssistanceDetailValidator()
    {
        RuleFor(u => u.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());

        RuleForEach(u => u.AssistanceDetailDto.AssistanceDetails)
            .MustBeValueObject(ua => AssistanceDetail.Create(
                ua.Name,
                ua.Description));
    }
}