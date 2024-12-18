using PetFamily.Application.Abstractions;
using PetFamily.Application.DTOs;
using PetFamily.Application.DTOs.ValueObject;

namespace PetFamily.Application.PetManagement.Commands.UpdateAssistanceDetail;

public record UpdateAssistanceDetailCommand(
    Guid VolunteerId,
    AssistanceDetailListDto AssistanceDetailList) : ICommand;
