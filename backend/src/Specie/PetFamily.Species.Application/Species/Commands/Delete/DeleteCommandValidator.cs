using FluentValidation;

namespace PetFamily.Species.Application.Species.Commands.Delete;

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(d => d.SpeciesId).NotEmpty().NotNull();
    }
}