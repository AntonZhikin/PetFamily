using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetReopenStatus;

public record SetReopenStatusCommand(
    Guid UserId, Guid RequestId, string Comment) : ICommand;