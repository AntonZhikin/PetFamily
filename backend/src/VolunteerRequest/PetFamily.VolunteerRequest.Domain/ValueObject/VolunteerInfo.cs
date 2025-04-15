using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.VolunteerRequest.Domain;

public record VolunteerInfo
{
    public string Value { get; }

    public VolunteerInfo(string value)
    {
        Value = value;
    }

    public static Result<VolunteerInfo, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("VolunteerInfo");
        
        return new VolunteerInfo(value);
    }
}