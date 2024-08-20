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
        CountPetInHome countPetInHome, 
        CountPetFoundHome countPetFoundHomes, 
        CountPetHealing countPetHealing, 
        FullName fullName) 
        : base(volunteerId)
    {
        Descriptions = description;
        PhoneNumbers = phoneNumber;
        ExperienceYears = experienceYears;
        CountPetInHomes = countPetInHome;
        CountPetFoundHomes = countPetFoundHomes;
        CountPetHealing = countPetHealing;
        FullNames = fullName;
    }
    
    //public Guid Id { get; private set; }

    public FullName FullNames;

    public Description Descriptions;

    public ExperienceYear ExperienceYears;

    public CountPetInHome CountPetInHomes;

    public CountPetFoundHome CountPetFoundHomes;

    public CountPetHealing CountPetHealing;

    public PhoneNumber PhoneNumbers { get; private set; } = null!;

    public List<Pet> Pets { get; private set; } = [];

    public VoluunterSocialList SocDetails;

    public VoluunterReqList ReqListDetails;

    // public static Volunteer Create(VolunteerId volunteerId, Description descriptions, PhoneNumber phoneNumbers)
    // {
    //     return new Volunteer(volunteerId, descriptions, phoneNumbers);
    // }
}