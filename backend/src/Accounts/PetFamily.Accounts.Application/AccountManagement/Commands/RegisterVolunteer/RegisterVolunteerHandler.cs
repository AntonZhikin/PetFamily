using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Application.Models;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Domain.Accounts;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Application.AccountManagement.Commands.RegisterVolunteer;

public class RegisterVolunteerHandler : ICommandHandler<string, RegisterVolunteerCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IAccountsManager _accountManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ILogger<RegisterVolunteerHandler> _logger;
    private readonly IValidator<RegisterVolunteerCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterVolunteerHandler(
        UserManager<User> userManager,
        IAccountsManager accountManager,
        RoleManager<Role> roleManager,
        ILogger<RegisterVolunteerHandler> logger,
        IValidator<RegisterVolunteerCommand> validator,
        [FromKeyedServices(Modules.Accounts)]IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _accountManager = accountManager;
        _logger = logger;
        _validator = validator;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string, ErrorList>> Handle(RegisterVolunteerCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        if (user == null)
            throw new Exception("User not found");
        
        var volunteerRole = await _roleManager.FindByNameAsync(VolunteerAccount.RoleName)
                            ?? throw new ApplicationException("Volunteer role isn't found");
        
        var volunteerAccount = new VolunteerAccount(user);
        
        user.VolunteerAccount = volunteerAccount;
        
        user.AddRole(volunteerRole);
        
        var result = await _accountManager
            .CreateVolunteerAccount(volunteerAccount, cancellationToken);
        if (result.IsFailure)
            throw new Exception("Fail to create volunteer account");
        
        _logger.LogInformation(
            "Volunteer account was was created for user {userId}",
            command.UserId);
        
        return "Volunteer account was was created for user {userId}\", command.UserId";
    }
}