using PetFamily.Core.Abstractions;
using PetFamily.Kernel.ValueObject;
using PetFamily.Kernel.ValueObject.Ids;

namespace PetFamily.Species.Domain.SpeciesManagement.Entity;

public class Breed : Entity<BreedId>
{
    private Breed(BreedId id) : base(id)
    {
    }   
    public Name Name { get; private set; }
    
    public Breed(BreedId breedId, Name name) : base(breedId)
    {
        Name = name;
    }
}