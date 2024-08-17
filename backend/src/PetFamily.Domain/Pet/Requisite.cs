using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet;

public class Requisite : Entity<RequisiteId>
{
    private Requisite(RequisiteId requisiteId, string name, string title) : base(requisiteId)
    {
        Name = name;
        Title = title;
    }
    
    //public Guid Id { get; private set; }
    
    public string Name { get; private set; } = null!;

    public string Title { get; private set; } = null!;
}