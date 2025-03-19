using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.Core;

public record RoleName
{
    public string Value { get; }
    private RoleName(string value)
    {
        Value = value;
    }

    public static Result<RoleName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("PermissionCode");

        return new RoleName(value);
    }

    public static implicit operator string(RoleName name) => name.Value;
}