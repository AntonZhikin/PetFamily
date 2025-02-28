using CSharpFunctionalExtensions;
using PetFamily.Accounts.Domain.Accounts.ValueObjects;
using PetFamily.Core;
using PetFamily.Core.RolesPermissions;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Domain.Accounts;

public class VolunteerAccount
{
    //efcore
    private VolunteerAccount() { }
    public VolunteerAccount(AssistanceDetailList assistanceDetails, int experienceYear)
    {
        Id = Guid.NewGuid();
        //AssistanceDetails = assistanceDetails;
        ExperienceYear = experienceYear;
    }
    
    private VolunteerAccount(UserId id)
    {
        UserId = id;
    }
    
    public static RoleName ROLE = RoleName.Create("volunteer").Value;
    public Guid Id { get; private set; }
    public User User { get; private set; }
    public Guid UserId { get; private set; }
    public int ExperienceYear { get; private set; }
    
    public static Result<VolunteerAccount, Error> Create(UserId id) => new VolunteerAccount(id);
}