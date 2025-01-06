using PetFamily.Accounts.Application.AccountManagement.Commands.Login;

namespace PetFamily.Accounts.Presentation.Request;

public record LoginUserRequest(string Email, string Password)
{
    public LoginCommand ToCommand() => new(Email, Password); 
}