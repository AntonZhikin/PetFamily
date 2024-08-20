using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer;

public record PhoneNumber
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LENGHT)
            return Errors.General.ValueIsInvalid("PhoneNumber");
        
        return new PhoneNumber(value);
    }
}