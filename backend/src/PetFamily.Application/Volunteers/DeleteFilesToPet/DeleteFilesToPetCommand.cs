using PetFamily.Application.Volunteers.DTOs.Validator;

namespace PetFamily.Application.Volunteers.DeleteFilesToPet;

public record DeleteFilesToPetCommand(Guid VolunteerId, Guid PetId);