namespace PetFamily.Domain.Pet;

public class AddressValue
{
    public string City { get; }
    public string Street { get; }

    public AddressValue(string city, string street)
    {
        City = city;
        Street = street;
    }
}