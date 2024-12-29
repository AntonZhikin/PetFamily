using PetFamily.Core.Abstractions;

namespace PetFamily.Species.Application.Species.Commands.AddBreedToSpecies;

public record AddBreedToSpeciesCommand(Guid SpeciesId, string Name) : ICommand;