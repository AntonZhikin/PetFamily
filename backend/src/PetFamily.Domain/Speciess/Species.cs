using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Speciess;

public sealed class Species : Entity<SpeciesId>
{
    public Species(SpeciesId id) : base(id)
    {

    }

    public string Specie { get; }
    
    private Species(SpeciesId speciesId, string species) : base(speciesId)
    {
        Specie = species;
    }

    public BreedList Breeds { get; private set; }
}