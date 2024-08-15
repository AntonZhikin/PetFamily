using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet;

public class Requisite : Entity
{
    private Requisite(Guid id) : base(id)
    {
        Id = id;
    }
    
    public Guid Id { get; private set; }
    
    public string Name { get; private set; } = null!;

    public string Title { get; private set; } = null!;
}