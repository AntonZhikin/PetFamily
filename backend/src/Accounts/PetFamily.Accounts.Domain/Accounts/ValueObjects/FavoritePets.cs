using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Domain.Accounts.ValueObjects;

public record FavoritePets
{
    private FavoritePets()
    {
        
    }
    public string Value { get; }

    public FavoritePets(string value)
    {
        Value = value;
    }

    public static Result<FavoritePets, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LENGHT)
            return Errors.General.ValueIsInvalid("FavoritePets");
        
        return new FavoritePets(value);
    }
}