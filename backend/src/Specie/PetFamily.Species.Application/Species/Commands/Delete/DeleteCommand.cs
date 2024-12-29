using PetFamily.Core.Abstractions;

namespace PetFamily.Species.Application.Species.Commands.Delete;

public record DeleteCommand(Guid SpeciesId) : ICommand;
