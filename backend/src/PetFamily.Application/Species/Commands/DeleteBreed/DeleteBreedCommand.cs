using PetFamily.Application.Abstractions;

namespace PetFamily.Application.Species.DeleteBreed;

public record DeleteBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;
