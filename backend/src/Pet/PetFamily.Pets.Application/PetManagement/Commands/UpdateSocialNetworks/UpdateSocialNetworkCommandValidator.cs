using FluentValidation;
using PetFamily.Core;
using PetFamily.Core.Validation;
using PetFamily.Kernel;
using PetFamily.Pets.Domain.ValueObjects;

namespace PetFamily.Pets.Application.PetManagement.Commands.UpdateSocialNetworks;

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
