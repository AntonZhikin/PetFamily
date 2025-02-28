using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.Core.RolesPermissions;

public record UserId
{
    private UserId() { }
    public Guid Value { get; }
    public UserId(Guid value)
    {
        Value = value;
    }

    public static Result<UserId, Error> Create(Guid value)
    {
        return new UserId(value);
    }
    public static Result<UserId, Error> Create()
    {
        return new UserId(Guid.NewGuid());
    }

    public static implicit operator Guid(UserId id) => id.Value;
}