using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Kernel.ValueObject;

namespace PetFamily.Species.Application.Species.Commands.AddBreedToSpecies;

public class AddBreedToSpeciesCommandValidator : AbstractValidator<AddBreedToSpeciesCommand>
{
    public AddBreedToSpeciesCommandValidator()
    {
        RuleFor(a => a.Name)
            .MustBeValueObject(Name.Create);
    }
}