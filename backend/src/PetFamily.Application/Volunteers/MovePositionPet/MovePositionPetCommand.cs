using PetFamily.Domain.Pet.PetValueObject;

namespace PetFamily.Application.Volunteers.MovePositionPet;

public record MovePositionPetCommand(Guid VolunteerId, Guid PetId, int NewPosition);