using PetFamily.Domain.Volunteer.VolunteerValueObject;

namespace PetFamily.Domain.Volunteer.VolunteerList;

public record AssistanceDetailList
{
    public IReadOnlyList<AssistanceDetail> AssistanceDetails { get; }
    
    private AssistanceDetailList() { }
    public AssistanceDetailList(List<AssistanceDetail> assistanceDetails)
    {
        AssistanceDetails = assistanceDetails;
    }
}