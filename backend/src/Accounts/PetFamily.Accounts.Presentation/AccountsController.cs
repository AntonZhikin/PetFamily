using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Accounts.Application.AccountManagement.Commands;
using PetFamily.Accounts.Application.AccountManagement.Commands.Login;
using PetFamily.Accounts.Application.AccountManagement.Commands.Logout;
using PetFamily.Accounts.Application.AccountManagement.Commands.RefreshTokens;
using PetFamily.Accounts.Application.AccountManagement.Commands.Register;
using PetFamily.Accounts.Application.AccountManagement.Commands.RegisterVolunteer;
using PetFamily.Accounts.Application.AccountManagement.Queries.GetUserInformation;
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

    [HttpPost("volunteer/registration")]
    public async Task<IActionResult> Create(
        [FromBody] RegisterVolunteerRequest request,
        [FromServices] RegisterVolunteerHandler handler,
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
    
    [HttpGet("user-info/{id:guid}")]
    public async Task<IActionResult> GetUserById(
        [FromServices] GetUserInformationHandler handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetUserInformationQuery(id);
        
        var result = await handler.Handle(query, cancellationToken);

        return Ok(result);
    }
    
    [HttpPost("Test")]
    public async Task<IActionResult> Test()
    {
        return Ok(200);
    }
}