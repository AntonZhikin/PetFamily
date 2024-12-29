using PetFamily.Core.Abstractions;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteSoftPet;

public record DeleteSoftPetCommand(Guid VolunteerId, Guid PetId) : ICommand;
