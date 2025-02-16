using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace PetFamily.Accounts.Infrastructure.Permission;

public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddRequirements(new PermissionAttribute(policyName))
            .Build();
        
        return Task.FromResult<AuthorizationPolicy?>(policy);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => 
        Task.FromResult<AuthorizationPolicy>(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => 
        Task.FromResult<AuthorizationPolicy?>(null);
}