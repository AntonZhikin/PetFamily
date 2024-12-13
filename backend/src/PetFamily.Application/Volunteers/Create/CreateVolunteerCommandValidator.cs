using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetManagement.ValueObjects;
using Description = PetFamily.Domain.PetManagement.ValueObjects.Description;
using PhoneNumber = PetFamily.Domain.PetManagement.ValueObjects.PhoneNumber;

namespace PetFamily.Application.Volunteers.Create;

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
        
        RuleFor(c => c.ExperienceYears)
            .MustBeValueObject(ExperienceYear.Create);
        
        RuleForEach(c => c.SocialNetworkList.SocialNetworks)
            .MustBeValueObject(s => SocialNetwork.Create(s.Name, s.Link));
        
        RuleForEach(c => c.AssistanceDetailList.AssistanceDetails)
            .MustBeValueObject(s => AssistanceDetail.Create(s.Name, s.Description));
    }
}