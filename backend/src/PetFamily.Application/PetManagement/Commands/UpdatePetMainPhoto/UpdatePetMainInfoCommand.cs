using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagement.Commands.UpdatePetMainPhoto;

public record UpdatePetMainInfoCommand(Guid PhotoId, Guid VolunteerId, Guid PetId) : ICommand;
