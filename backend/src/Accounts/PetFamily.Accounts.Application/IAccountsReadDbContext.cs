using PetFamily.Core.DTOs.Accounts;

namespace PetFamily.Accounts.Application;

public interface IAccountsReadDbContext
{
    public IQueryable<UserDto> Users { get; }
}