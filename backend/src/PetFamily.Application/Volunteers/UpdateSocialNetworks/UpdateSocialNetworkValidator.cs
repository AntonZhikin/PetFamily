using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Application.Volunteers.DTOs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer.VolunteerValueObject;

namespace PetFamily.Application.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworkValidator : AbstractValidator<UpdateSocialNetworkRequest>
{
    public UpdateSocialNetworkValidator()
    {
        RuleFor(u => u.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        
        RuleForEach(u => u.SocialNetworkDto.SocialNetworks)
            .MustBeValueObject(us => SocialNetwork.Create(
                us.Name,
                us.Link));
    }
}
