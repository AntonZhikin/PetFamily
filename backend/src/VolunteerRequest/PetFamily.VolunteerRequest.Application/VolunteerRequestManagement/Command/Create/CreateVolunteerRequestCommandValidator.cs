using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.Create;

public class CreateVolunteerRequestValidator :
    AbstractValidator<CreateVolunteerRequestCommand>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(vr => vr.UserId)
            .NotNull()
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        
        RuleFor(vr => vr.FullName).MustBeValueObject(fn =>
            FullName.Create(fn.FirstName, fn.SecondName, fn.LastName));
        
        RuleFor(vr => vr.PhoneNumber)
            .NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}