using CSharpFunctionalExtensions;

namespace PetFamily.Kernel.ValueObject;

public record FullName
{
    //efcore
    private FullName() { }
    
    public string Name { get; }
    public string Surname { get; }
    public string SecondName { get; }

    private FullName(string name, string surname, string secondName)
    {
        Name = name;
        Surname = surname;
        SecondName = secondName;
    }

    public static Result<FullName, Error> Create(string name, string surname, string secondname)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid("Name");
        
        if(string.IsNullOrWhiteSpace(surname))
            return Errors.General.ValueIsInvalid("Surname");
        
        if(string.IsNullOrWhiteSpace(secondname))
            return Errors.General.ValueIsInvalid("Secondname");
        
        return new FullName(name, surname, secondname);
    }
}