namespace PetFamily.Domain.Volunteer;

public class CountPetInHome
{
    public string Value { get; }

    private CountPetInHome(string value)
    {
        Value = value;
    }

    public static CountPetInHome Create(string countPetInHome)
    {
        return new CountPetInHome(countPetInHome);
    }
}