using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer.VolunteerValueObject;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

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
        
        RuleFor(c => c.ExperienceYears)
            .MustBeValueObject(ExperienceYear.Create);
    }
}