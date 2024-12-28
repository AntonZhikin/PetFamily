using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Kernel.ValueObject;

namespace PetFamily.Species.Application.Species.Commands.Create;

public class CreateSpeciesCommandValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesCommandValidator()
    {
        RuleFor(c => c.Name)
            .MustBeValueObject(Name.Create);
    }
}