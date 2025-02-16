using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PetFamily.Accounts.Infrastructure.Permission;


public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    public PermissionRequirementHandler()
    {
        
    }
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionAttribute permission)
    {
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)!.Value;
    }
}