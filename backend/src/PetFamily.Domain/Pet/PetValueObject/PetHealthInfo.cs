namespace PetFamily.Domain.Pet;

public record PetHealthInfo
{
    public string Value { get; }

    private PetHealthInfo(string value)
    {
        Value = value;
    }
}