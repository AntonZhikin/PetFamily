using PetFamily.Accounts.Domain;

namespace PetFamily.Accounts.Application.AccountManagement;

public interface ITokenProvider
{
    string GenerateAccessToken(User user);
}