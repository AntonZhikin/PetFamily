using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.VolunteerRequest.Domain.ValueObject;

public record VolunteerRequestStatus
{
    public Status Value { get; }
    
    private VolunteerRequestStatus(Status value)
    {
        Value = value;
    }
    public static Result<VolunteerRequestStatus, Error> Create(Status volunteerRequestStatus)
    {
        var validVolunteerRequestStatus = new VolunteerRequestStatus(volunteerRequestStatus);
        
        return validVolunteerRequestStatus;
    }
    public static Result<VolunteerRequestStatus, Error> Create(string volunteerRequestStatus)
    {
        var result = Enum.TryParse(volunteerRequestStatus, out Status validStatus);

        if (!result)
            return  Errors.General.ValueIsInvalid("volunteerRequestStatus");
        
        return new VolunteerRequestStatus(validStatus);
    }
}

public enum Status
{
    Submitted,
    Rejected,
    RevisionRequired,
    Approved,
    OnReview
}