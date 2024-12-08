using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet.PetValueObject;

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
