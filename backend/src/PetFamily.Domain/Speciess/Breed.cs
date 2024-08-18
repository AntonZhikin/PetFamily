using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Speciess;

public class Breed : Entity<BreedId>
{
    public string Breeds { get; }
    
    private Breed(BreedId breedId, string breed) : base(breedId)
    {
        Breeds = breed;
    }
}