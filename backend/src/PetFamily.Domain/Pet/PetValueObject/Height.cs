namespace PetFamily.Domain.Pet;

public record Height
{
    public float Value { get; }

    public Height(float value)
    {
        Value = value;
    }
}