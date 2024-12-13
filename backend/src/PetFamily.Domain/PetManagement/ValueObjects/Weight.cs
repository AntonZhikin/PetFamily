using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Domain.PetManagement.ValueObjects;

public record Weight
{
    
    public const int MAX_LENGTH_Weight = 75;
    
    public float Value { get; }

    public Weight(float value)
    {
        Value = value;
    }
    
    public static Result<Weight, Error> Create(float value)
    {
        if (value > MAX_LENGTH_Weight)
            return Errors.General.ValueIsInvalid("Weight");
        
        return new Weight(value);
    }
}