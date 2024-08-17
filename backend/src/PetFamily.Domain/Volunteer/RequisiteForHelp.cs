using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer;

public class RequisiteForHelp : Entity
{
    private RequisiteForHelp(Guid id) : base(id)
    {
        Id = id;
    }
    
    public Guid Id { get; private set; }
    
    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;
}