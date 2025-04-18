namespace PetFamily.Accounts.Contracts;

public interface IAccountsContract
{
    Task<HashSet<string>> GetUserPermissionsCodes(Guid userId);
}