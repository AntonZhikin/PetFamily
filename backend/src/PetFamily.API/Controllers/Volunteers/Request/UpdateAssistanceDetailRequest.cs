using PetFamily.Application.DTOs;
using PetFamily.Application.DTOs.ValueObject;
using PetFamily.Application.PetManagement.Commands.UpdateAssistanceDetail;

namespace PetFamily.API.Controllers.Volunteers.Request;

public record UpdateAssistanceDetailRequest(AssistanceDetailListDto AssistanceList)
{
    public UpdateAssistanceDetailCommand ToCommand(Guid id) => new(id, AssistanceList);
}