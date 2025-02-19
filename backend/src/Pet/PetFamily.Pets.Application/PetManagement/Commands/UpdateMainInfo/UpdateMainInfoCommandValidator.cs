using FluentValidation;
using PetFamily.Core;
using PetFamily.Core.Validation;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject;
using PetFamily.Pets.Domain.ValueObjects;

namespace PetFamily.Pets.Application.PetManagement.Commands.UpdateMainInfo;

public class UpdateMainInfoCommandValidator : AbstractValidator<UpdateMainInfoCommand>
{
    public UpdateMainInfoCommandValidator()
    {
        RuleFor(r => r.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        
        RuleFor(c => new { c.Name, c.Surname, c.SecondName })
            .MustBeValueObject(x => FullName.Create(x.Name, x.Surname, x.SecondName));
        
        RuleFor(c => c.Descriptions)
            .MustBeValueObject(Description.Create);
        
        RuleFor(c => c.PhoneNumbers)
            .MustBeValueObject(PhoneNumber.Create);
    }
}