namespace PetFamily.Domain.Volunteer;

public record CountPetHealing
{
    public string Value { get; }

    private CountPetHealing(string value)
    {
        Value = value;
    }

    public static CountPetHealing Create(string countPetHealing)
    {
        return new CountPetHealing(countPetHealing);
    }
}