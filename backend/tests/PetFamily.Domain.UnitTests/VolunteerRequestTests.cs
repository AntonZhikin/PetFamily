using System;
using CSharpFunctionalExtensions;
using FluentAssertions;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.VolunteerRequest.Domain;
using PetFamily.VolunteerRequest.Domain.ValueObject;
using Xunit;

public class VolunteerRequestTests
{
    [Fact]
    public void Create_Should_Initialize_VolunteerRequest_With_Submitted_Status()
    {
        // arrange
        var requestId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var volunteerInfo = VolunteerInfo.Create("Test info").Value;

        // act
        var result = VolunteerRequest.Create(requestId, userId, volunteerInfo);

        // assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Status.Should().Be(VolunteerRequestStatus.Create(Status.Submitted).Value);
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
        request.TakeForReview(adminId);

        // assert
        request.AdminId.Should().Be(adminId);
        request.Status.Should().Be(VolunteerRequestStatus.Create(Status.OnReview).Value);
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
        request.Status.Should().Be(VolunteerRequestStatus.Create(Status.RevisionRequired).Value);
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
        request.Status.Should().Be(VolunteerRequestStatus.Create(Status.Rejected).Value);
    }

    [Fact]
    public void SetApprovedStatus_Should_Set_Admin_Status_And_Comment()
    {
        // arrange
        var request = CreateSampleRequest();
        var adminId = Guid.NewGuid();
        var comment = RejectionComment.Create("Well done").Value;

        // act
        request.SetApprovedStatus(adminId, comment);

        // assert
        request.AdminId.Should().Be(adminId);
        request.RejectionComment.Should().Be(comment);
        request.Status.Should().Be(VolunteerRequestStatus.Create(Status.Approved).Value);
    }

    private VolunteerRequest CreateSampleRequest()
    {
        var requestId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var volunteerInfo = VolunteerInfo.Create("Some info").Value;
        return VolunteerRequest.Create(requestId, userId, volunteerInfo).Value;
    }
}
