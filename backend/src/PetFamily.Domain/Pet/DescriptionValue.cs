namespace PetFamily.Domain.Pet;

public class DescriptionValue
{
    public string Description { get; }

    private DescriptionValue(string description)
    {
        Description = description;
    }
}