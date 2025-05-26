using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetApprovedStatus;

public record SetApprovedStatusCommand(
    Guid AdminId, Guid RequestId, string Comment) : ICommand;