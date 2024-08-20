namespace PetFamily.Domain.Volunteer;

public record ExperienceYear
{
    public string Value { get; }

    private ExperienceYear(string value)
    {
        Value = value;
    }

    public static ExperienceYear Create(string experienceYears)
    {
        return new ExperienceYear(experienceYears);
    }
}