using PetFamily.Application.Abstractions;
using PetFamily.Application.DTOs;

namespace PetFamily.Application.PetManagement.Commands.UpdateAssistanceDetail;

public record UpdateAssistanceDetailCommand(
    Guid VolunteerId,
    AssistanceDetailListDto AssistanceDetailList) : ICommand;
