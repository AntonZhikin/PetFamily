namespace PetFamily.Domain.Pet;

public record Address
{
    public string City { get; }
    public string Street { get; }

    public Address(string city, string street)
    {
        City = city;
        Street = street;
    }
}