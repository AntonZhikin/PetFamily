using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Kernel;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Queries.GetAllByUserId;

public class GetAllByUserIdValidator :
    AbstractValidator<GetAllByUserIdQuery>
{
    public GetAllByUserIdValidator()
    {
        RuleFor(r => r.UserId)
            .NotNull()
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        
        RuleFor(q => q.Page)
            .GreaterThanOrEqualTo(1)
            .WithError(Errors.General.ValueIsInvalid("Page"));

        RuleFor(q => q.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithError(Errors.General.ValueIsInvalid("PageSize"));
    }
}