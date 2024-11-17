using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer.VolunteerValueObject;

public record PhoneNumber
{
    public const int MAX_LENGHT_NUMBER = 100;
    
    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < MAX_LENGHT_NUMBER)
            return Errors.General.ValueIsInvalid("PhoneNumber");
        
        return new PhoneNumber(value);
    }
}