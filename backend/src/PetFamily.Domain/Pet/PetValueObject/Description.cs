namespace PetFamily.Domain.Pet;

public class Description
{
    public string Descriptions { get; }

    private Description(string description)
    {
        Descriptions = description;
    }
}