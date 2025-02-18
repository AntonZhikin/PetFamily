namespace PetFamily.Accounts.Domain.Accounts.ValueObjects;

public record AssistanceDetailList
{
    private AssistanceDetailList() { }
    
    public List<AssistanceDetail> AssistanceDetails { get; }
    
    public AssistanceDetailList(List<AssistanceDetail> assistanceDetails)
    {
        AssistanceDetails = assistanceDetails;
    }
}