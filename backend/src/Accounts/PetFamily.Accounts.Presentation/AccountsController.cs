using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Accounts.Application.AccountManagement.Commands;
using PetFamily.Accounts.Application.AccountManagement.Commands.Login;
using PetFamily.Accounts.Application.AccountManagement.Commands.Logout;
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

        if (result.IsFailure)
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

        HttpContext.Response.Cookies.Append("refreshToken", result.Value.RefreshToken.ToString());

        return Ok(result.Value);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokens(
        [FromServices] RefreshTokenHandler handler,
        CancellationToken cancellationToken
    )
    {
        if (!HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        {
            return Unauthorized();
        }

        var result = await handler.Handle(
            new RefreshTokenCommand(Guid.Parse(refreshToken)), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        HttpContext.Response.Cookies.Append("refreshToken", result.Value.RefreshToken.ToString());

        return Ok(result.Value);
    }
    
    [HttpPost("logout")]
    public Task<IActionResult> Logout(
        [FromServices] LogoutHandler handler,
        CancellationToken cancellationToken
    )
    {
        HttpContext.Response.Cookies.Delete("refreshToken");

        return Task.FromResult<IActionResult>(Ok());
    }
}