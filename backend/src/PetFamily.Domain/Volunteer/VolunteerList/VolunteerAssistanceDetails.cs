using PetFamily.Domain.Volunteer.VolunteerValueObject;

namespace PetFamily.Domain.Volunteer.VolunteerList;

public record VolunteerAssistanceDetails
{
    public IReadOnlyList<AssistanceDetail> AssistanceDetails { get; }
    
    private VolunteerAssistanceDetails() { }
    public VolunteerAssistanceDetails(List<AssistanceDetail> assistanceDetails)
    {
        AssistanceDetails = assistanceDetails;
    }
}