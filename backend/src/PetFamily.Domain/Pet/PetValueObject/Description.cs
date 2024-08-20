namespace PetFamily.Domain.Pet;

public record Description
{
    public string Value { get; }

    private Description(string value)
    {
        Value = value;
    }
}