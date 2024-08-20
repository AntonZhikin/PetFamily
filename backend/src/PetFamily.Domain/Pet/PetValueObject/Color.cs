namespace PetFamily.Domain.Pet;

public record Color
{
    public string Value { get; }

    private Color(string value)
    {
        Value = value;
    }
}