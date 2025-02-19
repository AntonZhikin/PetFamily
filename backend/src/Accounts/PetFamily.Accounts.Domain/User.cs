using Microsoft.AspNetCore.Identity;
using PetFamily.Accounts.Domain.Accounts.ValueObjects;
using PetFamily.Kernel.ValueObject;

namespace PetFamily.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    //efcore
    private User()
    {
    }
    
    private List<Role> _roles = [];
    
    //public SocialNetworkList SocialNetworkList { get; set; }
    
    public IReadOnlyList<Role> Roles => _roles;
    
    public string Email { get; private set; }
    
    //public FullName FullName { get; private set; }
    
    public static User CreateAdmin(string userName, string email, Role role)
    {
        return new User
        {
            UserName = userName,    
            Email = email,
            _roles = [role]
        };
    }
    
    public static User CreatePartisipant(string userName, string email)
    {
        return new User
        {
            UserName = userName,    
            Email = email
        };
    }
}