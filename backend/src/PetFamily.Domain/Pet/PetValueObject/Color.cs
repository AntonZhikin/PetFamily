namespace PetFamily.Domain.Pet;

public class Color
{
    public string Colors { get; }

    private Color(string color)
    {
        Colors = color;
    }
}