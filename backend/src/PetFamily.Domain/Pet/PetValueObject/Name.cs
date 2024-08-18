namespace PetFamily.Domain.Pet;

public class Name
{
    public string Names { get; }

    private Name(string name)
    {
        Names = name;
    }
}
