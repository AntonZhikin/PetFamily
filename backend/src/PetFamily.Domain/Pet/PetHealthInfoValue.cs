namespace PetFamily.Domain.Pet;

public class PetHealthInfoValue
{
    public string PetHealthInfo { get; }

    private PetHealthInfoValue(string petHealthInfo)
    {
        PetHealthInfo = petHealthInfo;
    }
}