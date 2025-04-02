using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PetFamily.Accounts.Domain.Accounts;
using PetFamily.Accounts.Domain.Accounts.ValueObjects;
using PetFamily.Core;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject;

namespace PetFamily.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    //efcore
    private User() { }

    private User(
        string email,
        string userName,
        Role role)
    {
        Email = email;
        UserName = userName;
        _roles = [role];
    }
    
    private List<Role> _roles = [];
    
    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();
    public ParticipantAccount? ParticipantAccount { get; set; }

    public AdminAccount? AdminAccount { get; set; }
    public static User CreateAdmin(string userName, string email, Role role)
    {
        return new User
        {
            UserName = userName,    
            Email = email,
            _roles = [role]
        };
    }
    
    public static User CreatePartisipant(string userName, string email, Role role)
    {
        return new User
        {
            UserName = userName,    
            Email = email,
            _roles = [role]
        };
    }
    
    public static Result<User, Error> Create(string userName, string email, Role role)
    {
        if (userName != null && email != null)
        {
            return new User
            {
                UserName = userName,    
                Email = email,
                _roles = [role]
            };
        }
        return Errors.General.ValueIsInvalid("User");
    }
}