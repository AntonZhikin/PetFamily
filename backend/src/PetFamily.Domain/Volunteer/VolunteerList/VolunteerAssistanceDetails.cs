namespace PetFamily.Domain.Volunteer.VolunteerList;

public record VolunteerAssistanceDetails
{
    private VolunteerAssistanceDetails() { }
    public VolunteerAssistanceDetails(IReadOnlyList<AssistanceDetail> assistanceDetails)
    {
        AssistanceDetails = assistanceDetails;
    }
    
    public IReadOnlyList<AssistanceDetail> AssistanceDetails { get; }
}