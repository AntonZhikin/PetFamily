using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer;
using PetFamily.Domain.Pet;

public class Volunteer : Entity<VolunteerId>
{
    private Volunteer(VolunteerId id) : base(id)
    {
        
    }
    
    public Volunteer(
        VolunteerId volunteerId, 
        Description description, 
        PhoneNumber phoneNumber, 
        ExperienceYear experienceYears, 
        FullName fullName) 
        : base(volunteerId)
    {
        Descriptions = description;
        PhoneNumbers = phoneNumber;
        ExperienceYears = experienceYears;
        FullNames = fullName;
    }
    
    //public Guid Id { get; private set; }

    public FullName FullNames;

    public Description Descriptions;

    public ExperienceYear ExperienceYears;

    private readonly List<Pet> _pets = [];

    public IReadOnlyList<Pet> Pet => _pets;
    
    public int GetPetsInHome() => _pets.Count(p => p.HelpStatus == HelpStatus.InHome);

    public int GetPetFoundHome() => _pets.Count(p => p.HelpStatus == HelpStatus.FoundHome);

    public int GetPetHealing() => _pets.Count(p => p.HelpStatus == HelpStatus.PetHealing);

    public PhoneNumber PhoneNumbers { get; private set; } = null!;

    public List<Pet> Pets { get; private set; } = [];

    public VoluunterSocialList SocDetails;

    public VoluunterReqList ReqListDetails;
}