using PetFamily.Core.Abstractions;
using PetFamily.Kernel;
using PetFamily.Kernel.BaseClasses;
using PetFamily.Kernel.ValueObject;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.Species.Domain.SpeciesManagement.AggregateRoot;

namespace PetFamily.Species.Domain.SpeciesManagement.Entity;

public class Breed : SoftDeletableEntity<BreedId>
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