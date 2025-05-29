using PetFamily.Core.Abstractions;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Commands.Create;

public record CreateCommand(Guid RelationId, Guid ReviewingUserId, Guid ApplicantUserId) : ICommand;