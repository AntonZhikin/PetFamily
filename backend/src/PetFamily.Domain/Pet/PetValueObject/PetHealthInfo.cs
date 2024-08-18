namespace PetFamily.Domain.Pet;

public class PetHealthInfo
{
    public string PetHealthInfos { get; }

    private PetHealthInfo(string petHealthInfo)
    {
        PetHealthInfos = petHealthInfo;
    }
}