using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagement.Commands.DeleteFilesToPet;

public record DeleteFilesToPetCommand(Guid VolunteerId, Guid PetId) : ICommand;