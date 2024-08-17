using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer;

public class RequisiteForHelp : Entity<RequisiteForHelpId>
{
    private RequisiteForHelp(RequisiteForHelpId requisiteForHelpId, string name, string description) : base(requisiteForHelpId)
    {
        Name = name;
        Description = description;
    }
    
    //public Guid Id { get; private set; }
    
    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;
}