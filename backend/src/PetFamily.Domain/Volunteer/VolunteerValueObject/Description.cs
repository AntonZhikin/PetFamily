using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer.VolunteerValueObject;

public record Description
{
    public string Value { get; }

    private Description(string value)
    {
        Value = value;
    }

    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < Constants.MAX_LENGHT)
            return Errors.General.ValueIsInvalid("Description");
        
        return new Description(value);
    }
}