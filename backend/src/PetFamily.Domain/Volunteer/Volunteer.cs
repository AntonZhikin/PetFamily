namespace PetFamily.Domain.Volunteer;

public class Volunteer
{
    public Guid Id { get; private set; }

    public string Fio { get; private set; } = null!;

    public string Descriptions { get; private set; } = null!;

    public int ExperienceYears { get; private set; } = 0!;

    public int CountPetsInHome { get; private set; } = 0!;

    public int CountPetsFoundHome { get; private set; } = 0!;

    public int CountPetsHealing { get; private set; } = 0!;

    public string PhoneNumbers { get; private set; } = null!;

    public List<string> CountPets { get; private set; } = [];

    public List<SocialMedia> SocialMedias { get; private set; } = [];

    public List<RequisiteForHelp> RequisiteForHelps { get; private set; } = [];
}