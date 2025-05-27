using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Application.Models;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Domain.Accounts;
using PetFamily.Accounts.Infrastructure.DbContexts;
using PetFamily.Core;
using PetFamily.Core.RolesPermissions;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Infrastructure.IdentityManager;

public class AccountsManagers(WriteAccountsDbContext writeAccountsDbContext) : IAccountsManager
{
    public async Task CreateAdminAccount(AdminAccount adminAccount, CancellationToken cancellationToken = default)
    {
        await writeAccountsDbContext.AdminAccounts.AddAsync(adminAccount, cancellationToken);
        await writeAccountsDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateParticipantAccount(
        ParticipantAccount participantAccount, CancellationToken cancellationToken)
    {
        writeAccountsDbContext.ParticipantAccounts.Add(participantAccount);
        await writeAccountsDbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<UnitResult<Error>> CreateVolunteerAccount(
        VolunteerAccount volunteerAccount, CancellationToken cancellationToken)
    {
        writeAccountsDbContext.VolunteerAccounts.Add(volunteerAccount);
        var result = await writeAccountsDbContext.SaveChangesAsync(cancellationToken);
        
        return result <= 0 ?
            Errors.General.ValueIsInvalid("could.not.create.volunteer_account") :
            Result.Success<Error>();
    }

    public async Task<Result<Role, Error>> GetRole(RoleName roleName)
    {
        var result = await writeAccountsDbContext.Roles 
            .FirstOrDefaultAsync(r => r.Name.ToLower() == roleName.Value.ToLower());
        if (result is null)
            return Errors.General.NotFound();  
       
        return result;
    }
    
    public async Task<Result<Role, Error>> GetRoleId(Guid roleId)
    {
        var result = await writeAccountsDbContext.Roles  
            .FirstOrDefaultAsync(r => r.Id == roleId);
        if (result is null)
            return Errors.General.NotFound();
        return result;
    }

    public async Task AddParticipant(ParticipantAccount participant)
    {
        await writeAccountsDbContext.ParticipantAccounts.AddAsync(participant);
    }
    public async Task<bool> AnyAdminAccountExists(CancellationToken cancellationToken = default)
    {
        return await writeAccountsDbContext.AdminAccounts.AnyAsync(cancellationToken);
    }
}