using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Species.Commands.AddBreedToSpecies;

public class AddBreedToSpeciesCommandValidator : AbstractValidator<AddBreedToSpeciesCommand>
{
    public AddBreedToSpeciesCommandValidator()
    {
        RuleFor(a => a.Name)
            .MustBeValueObject(Name.Create);
    }
}