using PetFamily.Core.Abstractions;

namespace PetFamily.Species.Application.Species.Commands.Create;

public record CreateSpeciesCommand(string Name) : ICommand;
