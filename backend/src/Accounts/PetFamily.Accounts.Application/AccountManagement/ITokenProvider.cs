using PetFamily.Accounts.Domain.DataModels;

namespace PetFamily.Accounts.Application.AccountManagement;

public interface ITokenProvider
{
    string GenerateAccessToken(User user);
}