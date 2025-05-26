using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.VolunteerRequest.Domain.ValueObject;

public record VolunteerInfo
{
    public const string DB_COLUMN_AGE = "age";
    private VolunteerInfo() { }
    public int Age { get; }

    private VolunteerInfo(int age)
    {
        Age = age;
    }

    public static Result<VolunteerInfo, Error> Create(int age)
    {
        if (string.IsNullOrWhiteSpace(age.ToString()))
            return Errors.General.ValueIsInvalid("VolunteerInfo");
        
        return new VolunteerInfo(age);
    }
}