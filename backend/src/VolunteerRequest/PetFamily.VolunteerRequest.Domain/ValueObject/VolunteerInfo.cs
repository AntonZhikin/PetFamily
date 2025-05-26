using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.VolunteerRequest.Domain.ValueObject;

public record VolunteerInfo
{
    private VolunteerInfo() { }
    public string Value { get; init; }

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