using CSharpFunctionalExtensions;
using PetFamily.Domain.Pet.PetValueObject;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Ids;
using PetFamily.Domain.Speciess.SpeciesID;

namespace PetFamily.Domain.SpeciesManagement.Entity;

public class Breed : Shared.Entity<BreedId>
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