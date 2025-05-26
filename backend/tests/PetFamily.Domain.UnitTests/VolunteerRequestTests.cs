using FluentAssertions;
using PetFamily.Kernel.ValueObject;
using PetFamily.VolunteerRequest.Domain.ValueObject;

namespace PetFamily.UnitTests;

public class VolunteerRequestTests
{
    [Fact]
    public void Create_Should_Initialize_VolunteerRequest_With_Submitted_Status()
    {
        // arrange
        var requestId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var volunteerInfo = VolunteerInfo.Create(1).Value;
        var fullname = FullName.Create("13341", "14", "4231").Value;

        // act
        var result = VolunteerRequest.Domain.VolunteerRequest.Create(requestId, userId, fullname, volunteerInfo);

        // assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Status.Should().Be(RequestStatus.Submitted);
        result.Value.UserId.Should().Be(userId);
        result.Value.VolunteerInfo.Should().Be(volunteerInfo);
    }

    [Fact]
    public void TakeForReview_Should_Set_Admin_And_Status_OnReview()
    {
        // arrange
        var request = CreateSampleRequest();
        var adminId = Guid.NewGuid();

        // act
        request.TakeInReview(adminId);

        // assert
        request.AdminId.Should().Be(adminId);
        request.Status.Should().Be(RequestStatus.OnReview);
    }

    [Fact]
    public void SetRevisionRequiredStatus_Should_Set_Admin_Status_And_Comment()
    {
        // arrange
        var request = CreateSampleRequest();
        var adminId = Guid.NewGuid();
        var comment = RejectionComment.Create("Needs revision").Value;

        // act
        request.SetRevisionRequiredStatus(adminId, comment);

        // assert
        request.AdminId.Should().Be(adminId);
        request.RejectionComment.Should().Be(comment);
        request.Status.Should().Be(RequestStatus.RevisionRequired);
    }

    [Fact]
    public void SetRejectStatus_Should_Set_Admin_Status_And_Comment()
    {
        // arrange
        var request = CreateSampleRequest();
        var adminId = Guid.NewGuid();
        var comment = RejectionComment.Create("Not acceptable").Value;

        // act
        request.SetRejectStatus(adminId, comment);

        // assert
        request.AdminId.Should().Be(adminId);
        request.RejectionComment.Should().Be(comment);
        request.Status.Should().Be(RequestStatus.Rejected);
    }

    [Fact]
    public void SetApprovedStatus_Should_Set_Admin_Status_And_Comment()
    {
        // arrange
        var request = CreateSampleRequest();
        var adminId = Guid.NewGuid();
        var comment = "Well done";

        // act
        request.SetApprovedStatus(adminId, comment);

        // assert
        request.AdminId.Should().Be(adminId);
        request.RejectionComment.Should().Be(comment);
        request.Status.Should().Be(RequestStatus.Approved);
    }

    private VolunteerRequest.Domain.VolunteerRequest CreateSampleRequest()
    {
        var requestId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var volunteerInfo = VolunteerInfo.Create(1).Value;
        var fullname = FullName.Create("13341", "14", "4231").Value;
        return VolunteerRequest.Domain.VolunteerRequest.Create(requestId, userId,fullname, volunteerInfo).Value;
    }
}