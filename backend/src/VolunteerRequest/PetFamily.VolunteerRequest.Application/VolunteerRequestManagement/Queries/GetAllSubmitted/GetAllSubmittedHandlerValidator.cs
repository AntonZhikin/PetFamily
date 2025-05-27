using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Kernel;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Queries.GetAllSubmitted;


public class GetAllSubmittedValidator :
    AbstractValidator<GetAllSubmittedQuery>
{
    public GetAllSubmittedValidator()
    {
        RuleFor(q => q.Page)
            .GreaterThanOrEqualTo(1)
            .WithError(Errors.General.ValueIsInvalid("Page"));

        RuleFor(q => q.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithError(Errors.General.ValueIsInvalid("PageSize"));
    }
}