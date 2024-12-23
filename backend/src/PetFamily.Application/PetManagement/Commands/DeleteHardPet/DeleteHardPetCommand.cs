using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagement.Commands.DeleteHardPet;

public record DeleteHardPetCommand(Guid VolunteerId, Guid PetId) : ICommand;