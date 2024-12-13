using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetManagement.ValueObjects;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.AddPet;

public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
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