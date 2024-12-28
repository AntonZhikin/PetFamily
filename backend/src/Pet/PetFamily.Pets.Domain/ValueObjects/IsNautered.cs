using CSharpFunctionalExtensions;
using PetFamily.Core;
using PetFamily.Kernel;

namespace PetFamily.Pets.Domain.ValueObjects;

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
