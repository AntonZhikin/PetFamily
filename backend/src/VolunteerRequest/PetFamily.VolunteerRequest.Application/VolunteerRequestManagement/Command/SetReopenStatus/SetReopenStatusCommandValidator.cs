using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Kernel;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetReopenStatus;

public class SetReopenStatusValidator :
    AbstractValidator<SetReopenStatusCommand>
{
    public SetReopenStatusValidator()
    {
        RuleFor(r => r.UserId)
            .NotNull()
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        
        RuleFor(r => r.RequestId)
            .NotNull()
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        
        RuleFor(r => r.Comment)
            .NotNull()
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}