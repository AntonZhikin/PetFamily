using CSharpFunctionalExtensions;
using PetFamily.Core;
using PetFamily.Kernel;

namespace PetFamily.Pets.Domain.ValueObjects;

public record Height
{
    public const int MAX_LENGTH_HEIGHT = 75;
    
    public float Value { get; }

    public Height(float value)
    {
        Value = value;
    }
    
    public static Result<Height, Error> Create(float value)
    {
        if (value > MAX_LENGTH_HEIGHT)
            return Errors.General.ValueIsInvalid("Height");
        
        return new Height(value);
    }
}