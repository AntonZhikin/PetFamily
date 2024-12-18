using PetFamily.Application.Abstractions;

namespace PetFamily.Application.Species.Create;

public record CreateSpeciesCommand(string Name) : ICommand;
