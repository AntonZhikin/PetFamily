namespace PetFamily.Domain.Pet;

public record SpeciesForPet
{
    public string SpeciesForPets { get; }

    public SpeciesForPet(string species)
    {
        SpeciesForPets = species;
    }
}