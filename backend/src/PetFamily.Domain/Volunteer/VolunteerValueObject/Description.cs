namespace PetFamily.Domain.Volunteer;

public record Description
{
    public string Value { get; }

    private Description(string value)
    {
        Value = value;
    }

    public static Description Create(string description)
    {
        return new Description(description);
    }
}