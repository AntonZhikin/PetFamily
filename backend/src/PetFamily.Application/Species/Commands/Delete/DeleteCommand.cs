using PetFamily.Application.Abstractions;

namespace PetFamily.Application.Species.Delete;

public record DeleteCommand(Guid SpeciesId) : ICommand;
