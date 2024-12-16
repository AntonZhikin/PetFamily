using PetFamily.Application.Abstractions;

namespace PetFamily.Application.Species.AddBreedToSpecies;

public record AddBreedToSpeciesCommand(Guid SpeciesId, string Name) : ICommand;