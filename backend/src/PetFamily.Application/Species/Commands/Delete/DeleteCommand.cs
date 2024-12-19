using PetFamily.Application.Abstractions;

namespace PetFamily.Application.Species.Commands.Delete;

public record DeleteCommand(Guid SpeciesId) : ICommand;
