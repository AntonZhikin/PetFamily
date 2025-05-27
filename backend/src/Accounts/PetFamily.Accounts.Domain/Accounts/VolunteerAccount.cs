using CSharpFunctionalExtensions;
using PetFamily.Accounts.Domain.Accounts.ValueObjects;
using PetFamily.Core;
using PetFamily.Core.RolesPermissions;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Domain.Accounts;

public class VolunteerAccount
{
    //efcore
    private VolunteerAccount(){}
    public VolunteerAccount(User user)
    {
        Id = Guid.NewGuid();
        User = user;
        UserId = user.Id;
    }
    public const string RoleName = "Volunteer";
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid Id { get; set; }
}