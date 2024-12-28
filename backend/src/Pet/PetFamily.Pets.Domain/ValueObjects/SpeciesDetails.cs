using CSharpFunctionalExtensions;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject.Ids;

namespace PetFamily.Pets.Domain.ValueObjects;

public record SpeciesDetails
{
    public SpeciesId SpeciesId { get; }
    public BreedId BreedId { get; }

    // ef
    public SpeciesDetails() {}

    public SpeciesDetails(SpeciesId speciesId, BreedId breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }

    public static Result<SpeciesDetails, ErrorList> Create(SpeciesId speciesId, BreedId breedId)
    {
        return new SpeciesDetails(speciesId, breedId);
    }
}