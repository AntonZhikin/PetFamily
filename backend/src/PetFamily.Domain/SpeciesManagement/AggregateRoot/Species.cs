using CSharpFunctionalExtensions;
using PetFamily.Domain.Pet.PetValueObject;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Entity;
using PetFamily.Domain.Speciess.SpeciesID;

namespace PetFamily.Domain.SpeciesManagement.AggregateRoot;

public class Species : Shared.Entity<SpeciesId>
{
    //ef core
    private Species(SpeciesId id) : base(id)
    {
    }

    public Species(
        SpeciesId id,
        Name name,
        IEnumerable<Breed>? breed = null) : base(id)
    {
        Name = name;
        _breeds = new List<Breed>();
    }

    public Name Name { get; }

    private List<Breed> _breeds = [];

    public IReadOnlyList<Breed> Breeds => _breeds;

    public UnitResult<Error> AddBreed(Breed breed)
    {
        _breeds.Add(breed);

        return Result.Success<Error>();
    }
}
