using PetFamily.Accounts.Application.AccountManagement.Commands.RegisterVolunteer;

namespace PetFamily.Accounts.Presentation.Request;

public record RegisterVolunteerRequest(Guid UserId)
{
    public RegisterVolunteerCommand ToCommand() => new(UserId); 
}