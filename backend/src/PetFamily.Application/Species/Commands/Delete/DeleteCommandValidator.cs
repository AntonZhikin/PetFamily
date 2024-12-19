using FluentValidation;

namespace PetFamily.Application.Species.Delete;

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(d => d.SpeciesId).NotEmpty().NotNull();
    }
}