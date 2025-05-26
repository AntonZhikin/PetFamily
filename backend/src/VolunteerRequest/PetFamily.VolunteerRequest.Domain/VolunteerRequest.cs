using CSharpFunctionalExtensions;
using PetFamily.Kernel;
using PetFamily.Kernel.BaseClasses;
using PetFamily.Kernel.ValueObject;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.VolunteerRequest.Domain.ValueObject;

namespace PetFamily.VolunteerRequest.Domain;

public class VolunteerRequest : SoftDeletableEntity<VolunteerRequestId>
{
    private VolunteerRequest(VolunteerRequestId id) : base(id) { }
    
    public Guid? AdminId { get; private set; }
    public Guid UserId { get; private set; }
    
    public Guid DiscussionId { get; private set; }
    public VolunteerInfo VolunteerInfo { get; private set; }
    public FullName FullName { get; private set; }
    public RejectionComment? RejectionComment { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public RequestStatus Status { get; private set; }
    public VolunteerRequest(
        VolunteerRequestId id,
        Guid userId,
        FullName fullName,
        VolunteerInfo volunteerInfo) : base(id)
    {
        Id = id;
        UserId = userId;
        FullName = fullName;
        VolunteerInfo = volunteerInfo;
        Status = RequestStatus.Submitted;
    }
    
    private VolunteerRequest(
        VolunteerRequestId id,
        Guid userId,
        FullName fullName,
        VolunteerInfo volunteerInfo,
        RejectionComment rejectionComment,
        Guid adminId,
        Guid discussionId) : base(id)
    {
        Id = id;
        UserId = userId;
        FullName = fullName;
        VolunteerInfo = volunteerInfo;
        Status = RequestStatus.Submitted;
        RejectionComment = rejectionComment;
        CreatedAt = DateTime.UtcNow;
        AdminId = adminId;
        DiscussionId = discussionId;
    }

    
    public static Result<VolunteerRequest, Error> Create(
        VolunteerRequestId requestId, 
        Guid userId, 
        FullName fullName,
        VolunteerInfo volunteerInfo)
    {
        var request = new VolunteerRequest(requestId, userId, fullName, volunteerInfo);
        
        return request;
    }

    public void TakeInReview(Guid adminId)
    {
        AdminId = adminId;
        Status = RequestStatus.OnReview;
    }
    
    public void SetRevisionRequiredStatus(
        Guid adminId,
        RejectionComment rejectedComment)
    {
        Status = RequestStatus.RevisionRequired;
        RejectionComment = rejectedComment;
    }

    public void SetApprovedStatus(Guid adminId, string comment)
    {
        Status = RequestStatus.Approved;
        RejectionComment = null;
    }
    
    public void SetRejectStatus(
        Guid adminId,
        RejectionComment rejectedComment)
    {
        Status = RequestStatus.Rejected;
        RejectionComment = rejectedComment;
    }
    
    public void Refresh(
        Guid adminId, string message)
    {
        Status = RequestStatus.Submitted;
    }
}