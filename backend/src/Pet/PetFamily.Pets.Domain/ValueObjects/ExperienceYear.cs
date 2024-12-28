using CSharpFunctionalExtensions;
using PetFamily.Core;
using PetFamily.Kernel;

namespace PetFamily.Pets.Domain.ValueObjects;

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