using PetFamily.Kernel.ValueObject;

namespace PetFamily.Accounts.Domain.Accounts;

public class AdminAccount
{
    public const string ADMIN = nameof(ADMIN);
    
    //efcore
    private AdminAccount()
    {
        
    }
    public AdminAccount(Name name, User user)
    {
        Id = Guid.NewGuid();
        Name = name;
        User = user;
    }
    
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
    
    public Name Name { get; set; }
}