namespace PetFamily.Kernel.BaseClasses;

public abstract class SoftDeletableEntity<TId> : Entity<TId> where TId: notnull
{
    
    protected SoftDeletableEntity(TId id) : base(id) { }

    public virtual void Delete()
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