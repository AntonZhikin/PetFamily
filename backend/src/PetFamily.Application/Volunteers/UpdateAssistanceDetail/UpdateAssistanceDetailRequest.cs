using PetFamily.Application.Volunteers.DTOs;

namespace PetFamily.Application.Volunteers.UpdateAssistanceDetail;

public record UpdateAssistanceDetailRequest(
    Guid VolunteerId,
    UpdateAssistanceDetailDto AssistanceDetailDto);

public record UpdateAssistanceDetailDto(
    List<AssistanceDetailDto> AssistanceDetails);