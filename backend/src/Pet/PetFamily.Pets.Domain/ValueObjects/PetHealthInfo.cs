using CSharpFunctionalExtensions;
using PetFamily.Core;
using PetFamily.Kernel;

namespace PetFamily.Pets.Domain.ValueObjects;

public record PetHealthInfo
{
    public const int MAX_LENGHT_PetHealthInfo = 50;

    public string Value { get; }

    private PetHealthInfo(string value)
    {
        Value = value;
    }

    public static Result<PetHealthInfo, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGHT_PetHealthInfo)
            return Errors.General.ValueIsInvalid("PetHealthInfo");

        return new PetHealthInfo(value);
    }
}
