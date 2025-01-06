using PetFamily.Accounts.Application.AccountManagement.DataModels;

namespace PetFamily.Accounts.Application.AccountManagement;

public interface ITokenProvider
{
    string GenerateAccessToken(User user);
}