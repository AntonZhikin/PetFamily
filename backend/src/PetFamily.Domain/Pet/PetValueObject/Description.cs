using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet.PetValueObject;

public record Description
{
    public const int MAX_LENGTH_DESCRIPTION = 250;
    public string Value { get; }

    private Description(string value)
    {
        Value = value;
    }
    
    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH_DESCRIPTION)
            return Errors.General.ValueIsInvalid("Description");
        
        return new Description(value);
    }
}