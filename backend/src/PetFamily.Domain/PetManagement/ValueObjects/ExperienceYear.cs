using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Domain.PetManagement.ValueObjects;

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