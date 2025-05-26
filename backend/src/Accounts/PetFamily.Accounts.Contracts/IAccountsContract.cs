namespace PetFamily.Accounts.Contracts;

public interface IAccountsContract
{
    public Task<HashSet<string>> GetUserPermissionsCodes(Guid userId);
    
    public Task BanUser(Guid userId, CancellationToken cancellationToken);
    public Task<bool> IsUserBannedForVolunteerRequests(
        Guid userId, CancellationToken cancellationToken);
}