using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Application.Models;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Domain.Accounts;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Application.AccountManagement.Commands.Register;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RegisterUserHandler> _logger;
    private readonly RoleManager<Role> _roleManager;
    private readonly IAccountsManager _accountsManager;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserHandler(
        UserManager<User> userManager, 
        ILogger<RegisterUserHandler> logger,
        RoleManager<Role> roleManager,
        IAccountsManager accountsManager,
        [FromKeyedServices(Modules.Accounts)]IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _logger = logger;
        _roleManager = roleManager;
        _accountsManager = accountsManager;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterUserCommand command, CancellationToken cancellationToken = default)
    {
        /*var participantRole = await _roleManager.FindByNameAsync(ParticipantAccount.RoleName)
                              ?? throw new ApplicationException("Participant role is not found");
        
        var user = User.CreatePartisipant(command.UserName, command.Email, participantRole!);
        
        var result = await _userManager.CreateAsync(user, command.Password);
        
        var participantAccount = new ParticipantAccount(user);
        await _accountsManager.CreateParticipantAccount(participantAccount, cancellationToken);
        
        user.ParticipantAccount = participantAccount;
        
        await _userManager.UpdateAsync(user);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        var errors = result.Errors
            .Select(e => Error.Failure(e.Code, e.Description)).ToList();
        
        return new ErrorList(errors);*/
        
        var roleResult = await _accountsManager.GetRole(ParticipantAccount.ROLE);
        if (roleResult.IsFailure)
            return roleResult.Error.ToErrorList(); 
        
        var role = roleResult.Value;
        
        var user = User.Create(role, command.Email, command.Password);
        
        var result = await _userManager.CreateAsync(user, command.Password);

        var participant = ParticipantAccount.Create(user).Value;
        await _accountsManager.AddParticipant(participant);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Patrisipant с id = {0} добавлен", user.Id);
        var errors = result.Errors
            .Select(e => Error.Failure(e.Code, e.Description)).ToList();
        
        return new ErrorList(errors);
    }
}




