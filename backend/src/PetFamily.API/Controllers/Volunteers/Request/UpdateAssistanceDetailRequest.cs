using PetFamily.Application.Volunteers.DTOs;
using PetFamily.Application.Volunteers.UpdateAssistanceDetail;

namespace PetFamily.API.Controllers.Volunteers.Request;

public record UpdateAssistanceDetailRequest(AssistanceDetailListDto AssistanceList)
{
    public UpdateAssistanceDetailCommand ToCommand(Guid id) => new(id, AssistanceList);
}