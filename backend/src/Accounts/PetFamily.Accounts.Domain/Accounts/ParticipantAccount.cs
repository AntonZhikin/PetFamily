using PetFamily.Accounts.Domain.Accounts.ValueObjects;

namespace PetFamily.Accounts.Domain.Accounts;

public class ParticipantAccount
{
    //effcore
    private ParticipantAccount()
    {
    }
    
    public ParticipantAccount(User user, string favoritePets)
    {
        Id = Guid.NewGuid();
        User = user;
        FavoritePets = favoritePets;
        UserId = user.Id;
    }
    
    public const string PARTISIPANT = nameof(PARTISIPANT);
    
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    
    //public static string Role {get; private set;}
    
    public string FavoritePets { get; private set; }
    
    public User User { get; private set; }
}