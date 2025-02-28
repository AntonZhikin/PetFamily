using CSharpFunctionalExtensions;
using PetFamily.Accounts.Domain.Accounts.ValueObjects;
using PetFamily.Core;
using PetFamily.Core.RolesPermissions;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Domain.Accounts;

public class ParticipantAccount
{
    //effcore
    private ParticipantAccount() { }
    public ParticipantAccount(UserId userId)
    {
        UserId = userId;
    }
    public static RoleName ROLE = RoleName.Create("participant").Value;
    public UserId UserId { get; private set; }
    public User User { get; private set; }
    
    public static Result<ParticipantAccount, Error> Create(User user)
    {
        Role? role = user.Role;
        if (role.Name.ToLower() == ROLE)
        {
            UserId userId = UserId.Create(user.Id).Value;
            return new ParticipantAccount(userId);
        }
        return Errors.General.ValueIsInvalid();
    }
}