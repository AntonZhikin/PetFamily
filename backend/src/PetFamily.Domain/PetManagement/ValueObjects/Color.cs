using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet.PetValueObject;

public record Color
{
    public string Value { get; }

    private Color(string value)
    {
        Value = value;
    }
    
    public static Result<Color, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LENGHT)
            return Errors.General.ValueIsInvalid("Color");
        
        return new Color(value);
    }
}