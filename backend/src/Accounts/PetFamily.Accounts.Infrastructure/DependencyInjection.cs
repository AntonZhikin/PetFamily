using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Application.AccountManagement;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;
using PetFamily.Accounts.Infrastructure.IdentityManager;
using PetFamily.Accounts.Infrastructure.Options;
using PetFamily.Accounts.Infrastructure.Seeding;
using PetFamily.Framework.Authorization;

namespace PetFamily.Accounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration);
        services.AddAccountsSeeding();
        
        services.AddTransient<ITokenProvider, JwtTokenProvider>();
        
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.JWT));
        services.Configure<AdminOptions>(configuration.GetSection(AdminOptions.ADMIN));
    
        services
            .AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<WriteAccountsDbContext>()
            .AddDefaultTokenProviders(); 
        
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


        services.AddManagers();
        
        services.AddAuthorization();

        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        
        return services;
    }
    
    private static IServiceCollection AddDbContexts(this IServiceCollection services
        ,IConfiguration configuration)
    {
        services.AddScoped<WriteAccountsDbContext>(_ => 
            new WriteAccountsDbContext(configuration.GetConnectionString("Database")!));
        
        services.AddScoped<IAccountsReadDbContext, ReadAccountsDbContext>(_ => 
            new ReadAccountsDbContext(configuration.GetConnectionString("Database")!));
        
        return services;
    }
    
    private static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddScoped<PermissionManager>();
        services.AddScoped<RolePermissionManager>();
        services.AddScoped<AdminAccountManager>();
        
        return services;
    }
    
    private static IServiceCollection AddAccountsSeeding(this IServiceCollection services)
    {
        services.AddSingleton<AccountsSeeder>();
        services.AddScoped<AccountSeederService>();
        
        return services;
    }
}