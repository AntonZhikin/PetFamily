namespace PetFamily.Domain.Pet;

public class Info
{
    public static readonly Info NeedsHelp = new(nameof(NeedsHelp));
    public static readonly Info LookingForHome = new(nameof(LookingForHome));
    public static readonly Info FoundHome = new(nameof(FoundHome));

    public static readonly Info[] _all = [NeedsHelp, LookingForHome, FoundHome]; 
    
    public string Value { get; }

    private Info(string value)
    {
        Value = value;
    }
}