using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Kernel.ValueObject;
using PetFamily.Pets.Domain.ValueObjects;

namespace PetFamily.Pets.Application.PetManagement.Commands.Create;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    
    public CreateVolunteerCommandValidator()
    {
        RuleFor(c => new { c.Name, c.Surname, c.SecondName })
            .MustBeValueObject(x => FullName.Create(x.Name, x.Surname, x.SecondName));
        
        RuleFor(c => c.Descriptions)
            .MustBeValueObject(Description.Create);
        
        RuleFor(c => c.PhoneNumbers)
            .MustBeValueObject(PhoneNumber.Create);

    }
}