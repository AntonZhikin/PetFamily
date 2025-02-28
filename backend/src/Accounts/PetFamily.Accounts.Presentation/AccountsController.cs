using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Accounts.Application.AccountManagement.Commands;
using PetFamily.Accounts.Application.AccountManagement.Commands.Login;
using PetFamily.Accounts.Application.AccountManagement.Commands.RefreshTokens;
using PetFamily.Accounts.Application.AccountManagement.Commands.Register;
using PetFamily.Accounts.Contracts.Request;
using PetFamily.Accounts.Infrastructure;
using PetFamily.Accounts.Presentation.Request;
using PetFamily.Framework;
using PetFamily.Framework.Authorization;

namespace PetFamily.Accounts.Presentation;

public class AccountsController : ApplicationController
{
    [HttpPost("participant/registration")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler,
        CancellationToken cancellationToken
        )
    {
        var result = await handler.Handle(
            request.ToCommand(), cancellationToken);
        
        if(result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken cancellationToken
    )
    {
        var result = await handler.Handle(
            request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Login(
        [FromBody] RefreshTokenRequest request,
        [FromServices] RefreshTokenHandler handler,
        CancellationToken cancellationToken
    )
    {
        var result = await handler.Handle(
            new RefreshTokenCommand(request.AccessToken, request.RefreshToken), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}