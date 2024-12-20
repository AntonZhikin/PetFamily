using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagement.Commands.DeleteSoftPet;

public record DeleteSoftPetCommand(Guid VolunteerId, Guid PetId) : ICommand;
