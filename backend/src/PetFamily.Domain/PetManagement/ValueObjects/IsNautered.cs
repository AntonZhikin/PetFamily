using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Domain.PetManagement.ValueObjects;

public record IsNautered
{
    public bool Value { get; }

    public IsNautered(bool value)
    {
        Value = value;
    }
    
    public static Result<IsNautered, Error> Create(bool value)
    {
        return new IsNautered(value);
    }
}
