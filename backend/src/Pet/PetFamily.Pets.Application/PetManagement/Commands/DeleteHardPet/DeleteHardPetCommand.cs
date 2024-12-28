using PetFamily.Core.Abstractions;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteHardPet;

public record DeleteHardPetCommand(Guid VolunteerId, Guid PetId) : ICommand;