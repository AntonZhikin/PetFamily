namespace PetFamily.Domain.Volunteer;

public record FullName
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

    public static FullName Create(string name, string surname, string secondname)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        
        if(string.IsNullOrWhiteSpace(surname))
            throw new ArgumentNullException(nameof(surname));
        
        return new FullName(name, surname, secondname);
    }
}