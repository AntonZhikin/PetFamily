namespace PetFamily.Domain.Pet;

public class InfoValue
{
    public static readonly InfoValue NeedsHelp = new(nameof(NeedsHelp));
    public static readonly InfoValue LookingForHome = new(nameof(LookingForHome));
    public static readonly InfoValue FoundHome = new(nameof(FoundHome));

    public static readonly InfoValue[] _all = [NeedsHelp, LookingForHome, FoundHome]; 
    
    public string Value { get; }

    private InfoValue(string value)
    {
        Value = value;
    }
}