using PetFamily.Core.Abstractions;

namespace PetFamily.Accounts.Application.AccountManagement.Commands.RefreshTokens;

public record RefreshTokenCommand(string AccessToken, Guid RefreshToken) : ICommand;