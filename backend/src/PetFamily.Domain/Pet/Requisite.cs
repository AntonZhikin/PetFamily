using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet;

public sealed class Requisite : Entity<RequisiteId>
{
    //ef core
    private Requisite(RequisiteId id) : base(id)
    {
        
    }
    
    public string Name { get; private set; } = null!;

    public string Title { get; private set; } = null!;
}