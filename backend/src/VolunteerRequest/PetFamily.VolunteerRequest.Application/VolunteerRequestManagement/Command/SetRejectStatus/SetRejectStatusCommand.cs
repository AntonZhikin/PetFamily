using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetRejectStatus;

public record SetRejectStatusCommand(
    Guid AdminId, Guid RequestId, string Comment, bool IsBanNeed = false) : ICommand;