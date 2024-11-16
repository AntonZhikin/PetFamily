using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet.PetValueObject;

public record Address
{
    public string City { get; }
    public string Street { get; }

    public Address(string city, string street)
    {
        City = city;
        Street = street;
    }

    public static Result<Address, Error> Create(string city, string street)
    {
        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsInvalid("City");
        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValueIsInvalid("street");
        
        return new Address(city, street);
    }
}