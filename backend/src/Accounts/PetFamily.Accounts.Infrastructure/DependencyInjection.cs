using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Accounts.Application.AccountManagement;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Framework.Authorization;

namespace PetFamily.Accounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITokenProvider, JwtTokenProvider>();
        
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.JWT));

        services
            .AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AccountsDbContext>()
            .AddDefaultTokenProviders(); 

        services.AddDbContext(configuration);
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtOptions = configuration.GetSection(JwtOptions.JWT).Get<JwtOptions>() 
                                   ?? throw new ApplicationException("Missing jwt configuration"); 
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddSingleton<AccountsSeeder>();
        
        services.AddScoped<PermissionManager>();
        
        services.AddAuthorization();

        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        
        return services;
    }
    
    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddScoped<AccountsDbContext>(_ => 
            new AccountsDbContext(configuration.GetConnectionString("Database")!));

        return services;
    }
}