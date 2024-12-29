using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs.ValueObject;

namespace PetFamily.Pets.Application.PetManagement.Commands.UpdateAssistanceDetail;

public record UpdateAssistanceDetailCommand(
    Guid VolunteerId,
    AssistanceDetailListDto AssistanceDetailList) : ICommand;
