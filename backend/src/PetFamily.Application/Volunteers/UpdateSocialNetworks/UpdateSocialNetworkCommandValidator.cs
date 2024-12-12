using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Application.Volunteers.DTOs;
using PetFamily.Domain.PetManagement.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworkValidator : AbstractValidator<UpdateSocialNetworkCommand>
{
    public UpdateSocialNetworkValidator()
    {
        RuleFor(u => u.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        
        RuleForEach(u => u.SocialNetworkList.SocialNetworks)
            .MustBeValueObject(us => SocialNetwork.Create(
                us.Name,
                us.Link));
    }
}
