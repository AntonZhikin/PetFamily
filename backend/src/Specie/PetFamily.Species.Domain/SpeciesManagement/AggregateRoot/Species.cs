using CSharpFunctionalExtensions;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.Species.Domain.SpeciesManagement.Entity;

namespace PetFamily.Species.Domain.SpeciesManagement.AggregateRoot;

public class Species : SoftDeletableEntity<SpeciesId>
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
    
    public UnitResult<Error> DeleteBreed(Guid breedId)
    {
        var result = _breeds.FirstOrDefault(b => b.Id == breedId);
        if (result is null)
        {
            return Result.Success<Error>();
        }
        _breeds.Remove(result);

        return Result.Success<Error>();
    }

    public static Result<Species, ErrorList> Create(SpeciesId id, Name name)
    {
        var specie = new Species(id, name);
        
        return specie;
    }

    public override void SoftDelete()
    {
        base.SoftDelete();

        foreach (var breed in _breeds)
        {
            breed.SoftDelete();
        }
    }

    public void DeleteExpiredBreed()
    {
        _breeds.RemoveAll(
            i => i.DeletionDate != null 
                 && DateTime.UtcNow >= i.DeletionDate.Value
                    .AddDays(Constants.LIFETIME_AFTER_DELETION));
    } 
}
