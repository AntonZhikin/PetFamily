namespace PetFamily.Domain.Pet;

public class SpeciesValueObj
{
    public string Species { get; }

    public SpeciesValueObj(string species)
    {
        Species = species;
    }
}