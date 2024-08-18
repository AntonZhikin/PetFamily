namespace PetFamily.Domain.ValueObject;

public class Breed
{
    public string Breeds { get; }

    private Breed(string breed)
    {
        Breeds = breed;
    }
}