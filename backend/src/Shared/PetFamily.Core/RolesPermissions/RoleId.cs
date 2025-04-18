using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.Core.RolesPermissions;

public record RoleId
{
    public Guid Value { get; }

    private RoleId() { }
    public RoleId(Guid value)
    {
        Value = value;
    }

    public static Result<RoleId, Error> Create(Guid value)
    {
        return new RoleId(value);
    }

    public static implicit operator Guid(RoleId roleId) => roleId.Value;
}