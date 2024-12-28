using PetFamily.Core.DTOs.ValueObject;
using PetFamily.Pets.Application.PetManagement.Commands.UpdateAssistanceDetail;

namespace PetFamily.Pets.Controllers.Volunteers.Request;

public record UpdateAssistanceDetailRequest(AssistanceDetailListDto AssistanceList)
{
    public UpdateAssistanceDetailCommand ToCommand(Guid id) => new(id, AssistanceList);
}