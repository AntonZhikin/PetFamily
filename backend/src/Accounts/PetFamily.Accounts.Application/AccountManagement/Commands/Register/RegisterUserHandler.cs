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
using PetFamily.Kernel.ValueObject;

namespace PetFamily.Accounts.Application.AccountManagement.Commands.Register;

public class RegisterUserHandler : ICommandHandler<string, RegisterUserCommand>
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
    
    public async  Task<Result<string, ErrorList>> Handle(
        RegisterUserCommand command, CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var participantRole = await _roleManager.FindByNameAsync(ParticipantAccount.RoleName) 
                                  ?? throw new ApplicationException("Invalid PARTISIPANT");
        
            var userResult = User.Create(command.UserName, command.Email, participantRole);
            if(userResult.IsFailure)
                return Errors.General.Failure().ToErrorList();
        
            var result = await _userManager.CreateAsync(userResult.Value, command.Password);

            var participantAccount = new ParticipantAccount(userResult.Value);
        
            await _accountsManager.CreateParticipantAccount(participantAccount, cancellationToken);
        
            userResult.Value.ParticipantAccount = participantAccount;
        
            await _userManager.UpdateAsync(userResult.Value);
        
            await _unitOfWork.SaveChanges(cancellationToken);
            transaction.Commit();
        
            _logger.LogInformation("User was registered");
        
            return "User {username} was registered\", user.UserName";
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to register user {username}", command.UserName);
            transaction.Rollback();
        
            return Error.Failure("could.not.register.user", e.Message).ToErrorList();
        }
    }
}




