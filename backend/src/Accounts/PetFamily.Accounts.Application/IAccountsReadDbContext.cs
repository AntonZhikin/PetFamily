using PetFamily.Core.DTOs.Accounts;

namespace PetFamily.Accounts.Application;

public interface IAccountsReadDbContext
{
    public IQueryable<UserDto> Users { get; }
    
    public IQueryable<AdminAccountDto> AdminAccounts { get; }
    public IQueryable<VolunteerAccountDto> VolunteerAccounts { get; }
    public IQueryable<ParticipantAccountDto> ParticipantAccounts { get; }
}