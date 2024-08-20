namespace PetFamily.Domain.Pet;

public record Weight
{
    public float Value { get; }

    public Weight(float value)
    {
        Value = value;
    }
}