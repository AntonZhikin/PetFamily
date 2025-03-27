using CSharpFunctionalExtensions;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Domain.Accounts;
using PetFamily.Core;
using PetFamily.Core.RolesPermissions;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Application.Models;

public interface IAccountsManager
{
    public Task CreateAdminAccount(
        AdminAccount adminAccount, CancellationToken cancellationToken);
    public Task CreateParticipantAccount(
        ParticipantAccount participantAccount, CancellationToken cancellationToken = default);
    
    public Task<Result<Role, Error>> GetRole(RoleName roleName);
    public Task<Result<Role, Error>> GetRoleId(Guid roleId);
    public Task AddParticipant(ParticipantAccount participant);
}