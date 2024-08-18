namespace PetFamily.Domain.Volunteer;

public class FullName
{
    public string Name { get; }
    public string Surname { get; }
    public string SecondName { get; }

    private FullName(string name, string surname, string secondName)
    {
        Name = name;
        Surname = surname;
        SecondName = secondName;
    }
}