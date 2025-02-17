using Microsoft.AspNetCore.Identity;

namespace PetFamily.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    private User()
    {
    }
    
    private List<Role> _roles = [];
    public List<SocialNetwork> SocialNetworks { get; set; } = [];
    public IReadOnlyList<Role> Roles => _roles;

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