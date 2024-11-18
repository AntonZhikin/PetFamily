using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet.PetValueObject;

public record PhoneNumber
{
    public const int MAX_LENGHT_PhoneNumber = 60;

    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGHT_PhoneNumber)
            return Errors.General.ValueIsInvalid("PhoneNumber");

        return new PhoneNumber(value);
    }
}