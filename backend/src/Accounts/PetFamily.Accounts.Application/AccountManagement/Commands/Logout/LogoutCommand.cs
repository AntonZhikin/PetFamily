using PetFamily.Core.Abstractions;

namespace PetFamily.Accounts.Application.AccountManagement.Commands.Logout;

public record LogoutCommand(Guid RefreshToken) : ICommand;