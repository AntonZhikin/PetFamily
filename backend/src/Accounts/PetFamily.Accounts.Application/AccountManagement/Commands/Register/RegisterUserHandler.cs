using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Abstractions;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Application.AccountManagement.Commands.Register;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RegisterUserHandler> _logger;

    public RegisterUserHandler(
        UserManager<User> userManager, 
        ILogger<RegisterUserHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = User.CreatePartisipant(command.UserName, command.Email);
        
        var result = await _userManager.CreateAsync(user, command.Password);
        if (result.Succeeded)
        {
            _logger.LogInformation("User created: {username} a new account with password.", command.UserName);
            return Result.Success<ErrorList>();
        }

        await _userManager.AddToRoleAsync(user, "Participant");
        
        var errors = result.Errors
            .Select(e => Error.Failure(e.Code, e.Description)).ToList();
        
        return new ErrorList(errors);
    }
}