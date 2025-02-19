using PetFamily.Accounts.Domain.Accounts.ValueObjects;

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
    
    public const string VOLUNTEER = nameof(VOLUNTEER);
    public Guid Id { get; private set; }
    
    public Guid UserId { get; private set; }
    public int ExperienceYear { get; private set; }
    
    //public AssistanceDetailList AssistanceDetails { get; private set; }
    
    // public void UpdateAssistanceDetail(AssistanceDetailList assistanceDetailList)
    // {
    //     AssistanceDetails = assistanceDetailList;
    // }
}