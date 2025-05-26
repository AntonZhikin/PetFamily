using PetFamily.Core.Abstractions;

namespace PetFamily.Accounts.Application.AccountManagement.Commands.RegisterVolunteer;

public record RegisterVolunteerCommand(Guid UserId) : ICommand;