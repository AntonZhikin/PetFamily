using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Volunteer.VolunteerValueObject;
using Description = PetFamily.Domain.Pet.PetValueObject.Description;
using PhoneNumber = PetFamily.Domain.Pet.PetValueObject.PhoneNumber;

namespace PetFamily.Application.Volunteers.Create;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    
    public CreateVolunteerRequestValidator()
    {
        RuleFor(c => new { c.Name, c.Surname, c.SecondName })
            .MustBeValueObject(x => FullName.Create(x.Name, x.Surname, x.SecondName));
        
        RuleFor(c => c.Descriptions)
            .MustBeValueObject(Description.Create);
        
        RuleFor(c => c.PhoneNumbers)
            .MustBeValueObject(PhoneNumber.Create);
        
        RuleFor(c => c.ExperienceYears)
            .MustBeValueObject(ExperienceYear.Create);
        
        RuleForEach(c => c.SocialNetworks)
            .MustBeValueObject(s => SocialNetwork.Create(s.Name, s.Link));
        
        RuleForEach(c => c.AssistanceDetails)
            .MustBeValueObject(s => AssistanceDetail.Create(s.Name, s.Description));
    }
}