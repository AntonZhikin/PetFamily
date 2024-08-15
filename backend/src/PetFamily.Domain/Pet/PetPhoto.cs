using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet;

public class PetPhoto : Entity
{
    private PetPhoto(Guid id) : base(id)
    {
        Id = id;
    }
    
    public Guid Id { get; private set; }
    
    public string Path { get; private set; }
    
    public bool IsMain { get; private set; }
}