using PetFamily.Core.Abstractions;

namespace PetFamily.Accounts.Application.AccountManagement.Commands.Login;

public record LoginCommand(string Email, string Password) : ICommand;