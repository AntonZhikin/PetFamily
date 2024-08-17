namespace PetFamily.Domain.Pet;

public class BreedValue
{
    public string Breed { get; }

    private BreedValue(string breed)
    {
        Breed = breed;
    }
}