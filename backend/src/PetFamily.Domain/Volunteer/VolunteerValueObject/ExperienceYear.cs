using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer;

public record ExperienceYear
{
    public string Value { get; }

    private ExperienceYear(string value)
    {
        Value = value;
    }

    public static Result<ExperienceYear, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LENGHT)
            return Errors.General.ValueIsInvalid("ExperienceYear");
        
        return new ExperienceYear(value);
    }
}