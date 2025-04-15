using CSharpFunctionalExtensions;
using PetFamily.Core.RolesPermissions;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.VolunteerRequest.Domain.ValueObject;

namespace PetFamily.VolunteerRequest.Domain;

public class VolunteerRequest
{
    //efcore
    private VolunteerRequest() { }

    private VolunteerRequest(
        VolunteerRequestId id,
        Guid userId,
        VolunteerInfo volunteerInfo)
    {
        UserId = userId;
        VolunteerInfo = volunteerInfo;
        Status = VolunteerRequestStatus.Create(ValueObject.Status.Submitted).Value;
    }
    
    public Guid VolunteerRequestId { get; private set; }
    
    public Guid AdminId { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public Guid DiscussionId { get; private set; }
    
    public VolunteerInfo VolunteerInfo { get; private set; }
    
    public RejectionComment RejectionComment { get; private set; }
    
    public DateTime CreatedOn { get; private set; }
    
    public VolunteerRequestStatus Status { get; private set; }
    
    // Methods

    public static Result<VolunteerRequest, Error> Create(
        VolunteerRequestId requestId, 
        Guid userId, 
        VolunteerInfo volunteerInfo)
    {
        var request = new VolunteerRequest(requestId, userId, volunteerInfo);
        
        return request;
    }

    public void TakeForReview(Guid adminId)
    {
        AdminId = adminId;
        Status = VolunteerRequestStatus.Create(ValueObject.Status.OnReview).Value;
    }
    
    public void SetRevisionRequiredStatus(Guid adminId, RejectionComment rejectionComment)
    {
        AdminId = adminId;
        RejectionComment = rejectionComment;
        Status = VolunteerRequestStatus.Create(ValueObject.Status.RevisionRequired).Value;
    }

    public void SetRejectStatus(Guid adminId,RejectionComment rejectionComment)
    {
        AdminId = adminId;
        RejectionComment = rejectionComment;
        Status = VolunteerRequestStatus.Create(ValueObject.Status.Rejected).Value;
    }
    
    public void SetApprovedStatus(Guid adminId,RejectionComment rejectionComment)
    {
        AdminId = adminId;
        RejectionComment = rejectionComment;
        Status = VolunteerRequestStatus.Create(ValueObject.Status.Approved).Value;
    }
}