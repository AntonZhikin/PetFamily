using CSharpFunctionalExtensions;
using PetFamily.Accounts.Domain.Accounts.ValueObjects;
using PetFamily.Core;
using PetFamily.Core.RolesPermissions;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Domain.Accounts;

public class ParticipantAccount
{
    public const string RoleName = "Participant";
    
    private ParticipantAccount(){}
    public ParticipantAccount(User user)
    {
        Id = Guid.NewGuid();
        User = user;
        UserId = user.Id;
    }
    public DateTime? BannedForRequestsUntil { get; set; }
    public void BanForRequestsForWeek(DateTime date) =>
        BannedForRequestsUntil = date;
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid Id { get; set; }
}