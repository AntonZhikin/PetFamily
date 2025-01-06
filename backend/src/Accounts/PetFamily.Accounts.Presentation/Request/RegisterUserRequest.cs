using PetFamily.Accounts.Application.AccountManagement.Commands;
using PetFamily.Accounts.Application.AccountManagement.Commands.Register;

namespace PetFamily.Accounts.Presentation.Request;

public record RegisterUserRequest(string Email, string UserName, string Password)
{
    public RegisterUserCommand ToCommand() => new(Email, UserName, Password); 
}