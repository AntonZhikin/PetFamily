using PetFamily.Domain.Pet.PetValueObject;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer.VolunteerID;
using PetFamily.Domain.Volunteer.VolunteerList;
using PetFamily.Domain.Volunteer.VolunteerValueObject;
using Description = PetFamily.Domain.Volunteer.VolunteerValueObject.Description;
using PhoneNumber = PetFamily.Domain.Volunteer.VolunteerValueObject.PhoneNumber;

namespace PetFamily.Domain.Volunteer;
using PetFamily.Domain.Pet;

public class Volunteer : Entity<VolunteerId>
{
    public const int MAX_LENGHT = 100;
    
    private Volunteer(VolunteerId id) : base(id)
    {
        
    }
    
    public Volunteer(
        VolunteerId volunteerId, 
        Description description, 
        PhoneNumber phoneNumber, 
        ExperienceYear experienceYears, 
        FullName fullName,
        VolunteerSocialNetworks socialNetworks,
        VolunteerAssistanceDetails assistanceDetails
        ) 
        : base(volunteerId)
    {
        Descriptions = description;
        PhoneNumbers = phoneNumber;
        ExperienceYears = experienceYears;
        FullNames = fullName;
        SocialNetworks = socialNetworks;
        AssistanceDetails = assistanceDetails;
    }

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

    public VolunteerSocialNetworks SocialNetworks { get; }

    public VolunteerAssistanceDetails AssistanceDetails { get; }
}