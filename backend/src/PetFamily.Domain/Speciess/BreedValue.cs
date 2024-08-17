using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Speciess;

public class BreedValue : Entity<BreedId>
{
    public string Breed { get; }
    
    private BreedValue(BreedId breedId, string breed) : base(breedId)
    {
        Breed = breed;
    }
}