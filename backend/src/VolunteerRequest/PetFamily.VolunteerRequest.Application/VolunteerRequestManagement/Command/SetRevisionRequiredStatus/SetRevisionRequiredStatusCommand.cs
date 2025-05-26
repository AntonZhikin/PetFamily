using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetRevisionRequiredStatus;

public record SetRevisionRequiredStatusCommand(
    Guid AdminId, Guid RequestId, string Comment) : ICommand;