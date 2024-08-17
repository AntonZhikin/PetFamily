namespace PetFamily.Domain.Pet;

public class ColorValue
{
    public string Color { get; }

    private ColorValue(string color)
    {
        Color = color;
    }
}