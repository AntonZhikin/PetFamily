using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Domain.Volunteer;

public record Description
{
    public string Value { get; }

    private Description(string value)
    {
        Value = value;
    }

    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LENGHT)
            return Errors.General.ValueIsInvalid("Description");
        
        return new Description(value);
    }
}