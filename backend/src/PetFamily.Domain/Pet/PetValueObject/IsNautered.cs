namespace PetFamily.Domain.Pet;

public record IsNautered
{
    public bool Value { get; }

    public IsNautered(bool value)
    {
        Value = value;
    }
}