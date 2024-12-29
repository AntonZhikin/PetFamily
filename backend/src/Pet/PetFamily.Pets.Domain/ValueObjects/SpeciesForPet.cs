using CSharpFunctionalExtensions;
using PetFamily.Core;
using PetFamily.Kernel;

namespace PetFamily.Pets.Domain.ValueObjects;

public record SpeciesForPet
{
    public const int MAX_LENGHT_SpeciesForPet = 100;

    public string Value { get; }

    private SpeciesForPet(string value)
    {
        Value = value;
    }

    public static Result<SpeciesForPet, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < MAX_LENGHT_SpeciesForPet)
            return Errors.General.ValueIsInvalid("SpeciesForPet");

        return new SpeciesForPet(value);
    }
}