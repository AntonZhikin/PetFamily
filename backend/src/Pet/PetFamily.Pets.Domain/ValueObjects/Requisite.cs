using CSharpFunctionalExtensions;
using PetFamily.Core;
using PetFamily.Kernel;

namespace PetFamily.Pets.Domain.ValueObjects;

public record Requisite
{
    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    private Requisite(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public static Result<Requisite, Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired("Credential.Name");

        if (name.Length > Constants.MAX_LOW_TEXT_LENGHT)
            return Errors.General.ValueIsRequired("Credential.Name");

        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsRequired("Credential.Description");

        if (description.Length > Constants.MAX_LOW_TEXT_LENGHT)
            return Errors.General.ValueIsRequired("Credential.Description");

        return new Requisite(name, description);
    }
}