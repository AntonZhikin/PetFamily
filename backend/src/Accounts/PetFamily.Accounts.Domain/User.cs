using Microsoft.AspNetCore.Identity;
using PetFamily.Accounts.Domain.Accounts;
using PetFamily.Accounts.Domain.Accounts.ValueObjects;
using PetFamily.Core;
using PetFamily.Kernel.ValueObject;

namespace PetFamily.Accounts.Domain;

public sealed class User : IdentityUser<Guid>
{
    //efcore
    private User() { }
    private User(Role role, string email, string username)
    {
        Role = role;
        Email = email;
        UserName = username;
    }
    
    private List<Role> _roles = [];
    public RoleId RoleId { get; set; }
    
    public Role Role { get; set; }
    
    public static RoleName ROLE = RoleName.Create("user").Value;
    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();
    
    public AdminAccount? Admin { get; private set; }
    
    public VolunteerAccount? Volunteer { get; private set; }
    
    public ParticipantAccount? Participant { get; private set; }
    
    
    public string Email { get; private set; }
    
    public static User Create(Role role, string email, string password)
    {
        return new User(role, email, password);
    }
    
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
}