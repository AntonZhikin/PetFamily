using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Kernel.ValueObject;
using PetFamily.Pets.Domain.ValueObjects;

namespace PetFamily.Pets.Application.PetManagement.Commands.UpdatePet;

public class UpdatePetCommandValidator : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetCommandValidator()
    {
        RuleFor(a => a.SpeciesId).NotNull().NotEmpty();
        
        RuleFor(a => a.BreedId).NotNull().NotEmpty();
        
        RuleFor(a => a.Name)
            .MustBeValueObject(Name.Create);
        
        RuleFor(a => a.Description)
            .MustBeValueObject(Description.Create);
        
        RuleFor(a => a.Color)
            .MustBeValueObject(Color.Create);
        
        RuleFor(a => a.PetHealthInfo)
            .MustBeValueObject(PetHealthInfo.Create);
        
        RuleFor(a => new { a.City, a.Street })
            .MustBeValueObject(x => Address.Create(x.City, x.Street));
        
        RuleFor(a => a.Weight)
            .MustBeValueObject(Weight.Create);
        
        RuleFor(a => a.Height)
            .MustBeValueObject(Height.Create);
        
        RuleFor(a => a.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);

        RuleFor(a => a.IsNeutered).NotEmpty();
        RuleFor(a => a.HelpStatus).NotEmpty();
        RuleFor(a => a.DateCreate).NotEmpty();
        RuleFor(a => a.DateOfBirth).NotEmpty();
    }
}