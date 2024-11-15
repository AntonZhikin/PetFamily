using PetFamily.Domain.Shared;
using PetFamily.Domain.Speciess.SpeciesID;
using PetFamily.Domain.Speciess.SpeciesList;

namespace PetFamily.Domain.Speciess;

public sealed class Species : Entity<SpeciesId>
{
    //ef core
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