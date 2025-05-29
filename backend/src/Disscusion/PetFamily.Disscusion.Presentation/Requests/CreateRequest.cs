using PetFamily.Disscusion.Application.DiscussionManagement.Commands;
using PetFamily.Disscusion.Application.DiscussionManagement.Commands.Create;

namespace PetFamily.Disscusion.Presentation.Requests;

public record CreateRequest(Guid RelationId, Guid ReviewingId, Guid ApplicantId)
{
    public CreateCommand ToCommand() =>
        new(RelationId, ReviewingId, ApplicantId);
}