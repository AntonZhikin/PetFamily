using PetFamily.Domain.SpeciesManagement.Entity;

namespace PetFamily.Domain.SpeciesManagement.SpeciesList;

public record BreedList
{
    public List<Breed> Breeds { get; }

    public BreedList(IEnumerable<Breed> breeds)
    {
        Breeds = breeds.ToList();
    }
}