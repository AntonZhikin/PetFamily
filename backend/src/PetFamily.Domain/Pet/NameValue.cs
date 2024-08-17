namespace PetFamily.Domain.Pet;

public class NameValue
{
    public string Name { get; }

    private NameValue(string name)
    {
        Name = name;
    }
}
