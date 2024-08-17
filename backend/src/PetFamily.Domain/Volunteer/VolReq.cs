namespace PetFamily.Domain.Volunteer;

public record VolReq()
{
    public List<RequisiteForHelp> RequisiteForHelps { get; }
}