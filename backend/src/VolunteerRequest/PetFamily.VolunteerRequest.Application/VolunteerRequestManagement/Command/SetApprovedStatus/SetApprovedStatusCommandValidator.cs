using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Kernel;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetApprovedStatus;

public class SetApprovedStatusValidator :
    AbstractValidator<SetApprovedStatusCommand>
{
    public SetApprovedStatusValidator()
    {
        RuleFor(r => r.AdminId)
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