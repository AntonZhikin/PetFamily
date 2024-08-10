namespace PetFamily.Domain.Volunteer;
using PetFamily.Domain.Pet;

public class Volunteer
{
    public Guid Id { get; private set; }

    public string FullName { get; private set; } = null!;

    public string Descriptions { get; private set; } = null!;

    public int ExperienceYears { get; private set; }

    public int CountPetInHome { get; private set; }

    public int CountPetFoundHome { get; private set; }

    public int CountPetHealing { get; private set; }

    public string PhoneNumber { get; private set; } = null!;

    public List<Pet> Pets { get; private set; } = [];

    public List<SocialMedia> SocialMedias { get; private set; } = [];

    public List<RequisiteForHelp> RequisiteForHelps { get; private set; } = [];
}
