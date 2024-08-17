using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Speciess;

public class SpeciesValue : Entity<SpeciesId>
{
    public string Species { get; }
    
    private SpeciesValue(SpeciesId speciesId, string species) : base(speciesId)
    {
        Species = species;
    }

    public BreedList Breeds { get; private set; }
}