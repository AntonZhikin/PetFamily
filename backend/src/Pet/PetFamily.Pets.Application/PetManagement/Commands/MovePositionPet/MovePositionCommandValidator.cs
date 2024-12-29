using FluentValidation;

namespace PetFamily.Pets.Application.PetManagement.Commands.MovePositionPet;

public class MovePositionCommandValidator : AbstractValidator<MovePositionPetCommand>
{
    public MovePositionCommandValidator()
    {
        RuleFor(c => c.VolunteerId).NotNull();
        RuleFor(c => c.PetId).NotNull();
    }
}