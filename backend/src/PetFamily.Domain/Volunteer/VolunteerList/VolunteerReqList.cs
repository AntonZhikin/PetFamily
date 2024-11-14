namespace PetFamily.Domain.Volunteer;

public record VolunteerReqList()
{
    public List<RequisiteForHelp> RequisiteForHelps { get; }
}