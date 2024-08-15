namespace PetFamily.Domain.Pet;

public class Requisite
{
    public Guid Id { get; private set; }
    
    public string Name { get; private set; } = null!;

    public string Title { get; private set; } = null!;
}