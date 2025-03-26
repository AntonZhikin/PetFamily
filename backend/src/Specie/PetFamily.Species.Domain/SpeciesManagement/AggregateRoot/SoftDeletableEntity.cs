using PetFamily.Kernel;

namespace PetFamily.Species.Domain.SpeciesManagement.AggregateRoot;

public abstract class SoftDeletableEntity<TId> : Entity<TId> where TId: notnull
{
    protected SoftDeletableEntity(TId id) : base(id) { }

    public virtual void SoftDelete()
    {
        if (IsDeleted) return;
        
        IsDeleted = true;
        DeletionDate = DateTime.Now;
    }
    public virtual void Restore()
    {
        if (!IsDeleted) return;
        
        IsDeleted = true;
        DeletionDate = DateTime.Now;
    }
    
    public bool IsDeleted { get; private set; }
    public DateTime? DeletionDate { get; private set; }
    
}