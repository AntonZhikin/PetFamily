using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Queries;

public class GetByIdValidator :
    AbstractValidator<GetByIdQuery>
{
    public GetByIdValidator()
    {
        RuleFor(d => d.DiscussionId)
            .NotNull()
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}