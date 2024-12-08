using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Pet.PetValueObject;

namespace PetFamily.Application.Species.Create;

public class CreateSpeciesCommandValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesCommandValidator()
    {
        RuleFor(c => c.Name)
            .MustBeValueObject(Name.Create);
    }
}