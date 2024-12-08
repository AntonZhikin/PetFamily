using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet.PetValueObject;

public record Name
{
    public const int MAX_LENGHT_NAME = 50;
    
    public string Value { get; }

    private Name(string value)
    {
        Value = value;
    }

    public static Result<Name, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGHT_NAME)
            return Errors.General.ValueIsInvalid("Name");

        return new Name(value);
    }
}
