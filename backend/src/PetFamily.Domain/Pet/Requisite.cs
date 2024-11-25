using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet;

public record Requisite
{
    public string Name { get; private set; } = null!;

    public string Title { get; private set; } = null!;

    private Requisite(string name, string title)
    {
        Name = name;
        Title = title;
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