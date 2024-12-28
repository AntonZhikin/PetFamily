using PetFamily.Core.Abstractions;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteFilesToPet;

public record DeleteFilesToPetCommand(Guid VolunteerId, Guid PetId) : ICommand;