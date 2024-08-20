namespace PetFamily.Domain.Volunteer;

public class CountPetFoundHome
{
    public string Value { get; }

    private CountPetFoundHome(string value)
    {
        Value = value;
    }

    public static CountPetFoundHome Create(string countPetFoundHome)
    {
        return new CountPetFoundHome(countPetFoundHome);
    }
}