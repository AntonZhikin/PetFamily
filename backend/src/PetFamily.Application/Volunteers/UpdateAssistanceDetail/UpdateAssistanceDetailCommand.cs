using PetFamily.Application.Volunteers.DTOs;

namespace PetFamily.Application.Volunteers.UpdateAssistanceDetail;

public record UpdateAssistanceDetailCommand(
    Guid VolunteerId,
    AssistanceDetailListDto AssistanceDetailList);
