using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using PetFamily.Disscusion.Domain;
using PetFamily.Disscusion.Domain.AggregateRoot;
using PetFamily.Kernel;
using Message = PetFamily.Disscusion.Domain.Entity.Message;

namespace PetFamily.UnitTests;

public class DiscussionTests
    {
        /*[Fact]
        public void Create_Should_Initialize_Disscusion_With_Users_And_Open_Status()
        {
            // arrange
            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();
            var users = new List<Guid> { user1, user2 };

            // act
            var disscusion = new Discussion(users);

            // assert
            disscusion.Users.Should().Contain(new[] { user1, user2 });
            disscusion.Status.Should().Be(DiscussionStatus.Open);
        }

        [Fact]
        public void AddMessage_Should_Succeed_When_User_Is_Participant_And_Disscusion_Is_Open()
        {
            // arrange
            var userId = Guid.NewGuid();
            var disscusion = new Discussion(new List<Guid> { userId });
            var message = new Message(userId, "Hello");

            // act
            var result = disscusion.AddMessage(message);

            // assert
            result.IsSuccess.Should().BeTrue();
            disscusion.Messages.Should().ContainSingle(m => m.Content == "Hello");
        }

        [Fact]
        public void AddMessage_Should_Fail_When_Disscusion_Is_Closed()
        {
            // arrange
            var userId = Guid.NewGuid();
            var disscusion = new Discussion(new List<Guid> { userId });
            disscusion.Close();
            var message = new Message(userId, "Should fail");

            // act
            var result = disscusion.AddMessage(message);

            // assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(Errors.Discussion.ClosedDiscussion());
        }

        [Fact]
        public void AddMessage_Should_Fail_When_User_Is_Not_A_Participant()
        {
            // arrange
            var userId = Guid.NewGuid();
            var disscusion = new Discussion(new List<Guid> { Guid.NewGuid() }); // different user
            var message = new Message(userId, "Should fail");

            // act
            var result = disscusion.AddMessage(message);

            // assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(Errors.Discussion.UserNotInDiscussion());
        }

        [Fact]
        public void EditMessage_Should_Succeed_When_User_Is_Owner()
        {
            // arrange
            var userId = Guid.NewGuid();
            var disscusion = new Discussion(
                new List<Guid> { userId });
            var message = new Message(userId, "Old");
            disscusion.AddMessage(message);

            // act
            var result = disscusion.EditMessage(message.Id, userId, "New");

            // assert
            result.IsSuccess.Should().BeTrue();
            disscusion.Messages.Should().ContainSingle(m => m.Content == "New" && m.IsEdited);
        }

        [Fact]
        public void EditMessage_Should_Fail_When_User_Is_Not_Owner()
        {
            // arrange
            var userId = Guid.NewGuid();
            var otherUser = Guid.NewGuid();
            var disscusion = new Discussion(new List<Guid> { userId, otherUser });
            var message = new Message(userId, "Hello");
            disscusion.AddMessage(message);

            // act
            var result = disscusion.EditMessage(message.Id, otherUser, "Hacked");

            // assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(Errors.Discussion.UserNotInDiscussion());
        }

        [Fact]
        public void RemoveMessage_Should_Succeed_When_User_Is_Owner()
        {
            // arrange
            var userId = Guid.NewGuid();
            var disscusion = new Discussion(new List<Guid> { userId });
            var message = new Message(userId, "To be deleted");
            disscusion.AddMessage(message);

            // act
            var result = disscusion.RemoveMessage(message);

            // assert
            result.IsSuccess.Should().BeTrue();
            disscusion.Messages.Should().BeEmpty();
        }*/
    }