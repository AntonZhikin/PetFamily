using CSharpFunctionalExtensions;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.Species.Domain.SpeciesManagement.Entity;

namespace PetFamily.Species.Domain.SpeciesManagement.AggregateRoot;

public class Species : Core.Abstractions.Entity<SpeciesId>
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
    
    public Result<Guid, ErrorList> DeleteBreed(Guid breedId)
    {
        var result = _breeds.FirstOrDefault(b => b.Id == breedId);
        _breeds.Remove(result);

        return result.Id.Value;
    }

    public static Result<Species, ErrorList> Create(SpeciesId id, Name name)
    {
        var specie = new Species(id, name);
        
        return specie;
    }
}
