namespace PetFamily.Domain.Volunteer;

public record PhoneNumber
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static PhoneNumber Create(string phonenumber)
    {
        return new PhoneNumber(phonenumber);
    }
}