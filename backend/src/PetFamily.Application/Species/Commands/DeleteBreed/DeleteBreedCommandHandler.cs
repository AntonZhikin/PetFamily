using FluentValidation;

namespace PetFamily.Application.Species.Commands.DeleteBreed;

public class DeleteBreedCommandHandler : AbstractValidator<DeleteBreedCommand>
{
    public DeleteBreedCommandHandler()
    {
        RuleFor(d => d.SpeciesId).NotNull().NotEmpty();
        RuleFor(d => d.BreedId).NotNull().NotEmpty();
    }
}
