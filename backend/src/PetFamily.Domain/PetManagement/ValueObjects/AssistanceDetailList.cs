namespace PetFamily.Domain.PetManagement.ValueObjects;

public record AssistanceDetailList
{
    public IReadOnlyList<AssistanceDetail> AssistanceDetails { get; }
    
    private AssistanceDetailList() { }
    public AssistanceDetailList(List<AssistanceDetail> assistanceDetails)
    {
        AssistanceDetails = assistanceDetails;
    }
}