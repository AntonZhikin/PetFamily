namespace PetFamily.Domain.Volunteer;

public class FullNameValue
{
    public string Name { get; }
    public string Surname { get; }
    public string SecondName { get; }

    private FullNameValue(string name, string surname, string secondName)
    {
        Name = name;
        Surname = surname;
        SecondName = secondName;
    }
}