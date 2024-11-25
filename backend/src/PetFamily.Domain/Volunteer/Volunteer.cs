using PetFamily.Domain.Pet.PetValueObject;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer.VolunteerID;
using PetFamily.Domain.Volunteer.VolunteerList;
using PetFamily.Domain.Volunteer.VolunteerValueObject;
using Description = PetFamily.Domain.Volunteer.VolunteerValueObject.Description;
using PhoneNumber = PetFamily.Domain.Volunteer.VolunteerValueObject.PhoneNumber;

namespace PetFamily.Domain.Volunteer;
using PetFamily.Domain.Pet;

public class Volunteer : Entity<VolunteerId>//, ISoftDeletable
{
    public const int MAX_LENGHT = 100;

    public bool _isDeleted { get; private set; } = false;
    
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
        Description = description;
        PhoneNumber = phoneNumber;
        ExperienceYear = experienceYears;
        FullName = fullName;
        SocialNetworks = socialNetworks;
        AssistanceDetails = assistanceDetails;
    }

    public FullName FullName { get; private set; }

    public Description Description { get; private set; }

    public ExperienceYear ExperienceYear { get; private set; }

    private readonly List<Pet> _pets = [];

    public IReadOnlyList<Pet> Pet => _pets;
    
    public int GetPetsInHome() => _pets.Count(p => p.HelpStatus == HelpStatus.InHome);
    
    public int GetPetFoundHome() => _pets.Count(p => p.HelpStatus == HelpStatus.FoundHome);

    public int GetPetHealing() => _pets.Count(p => p.HelpStatus == HelpStatus.PetHealing);

    public PhoneNumber PhoneNumber;

    public List<Pet> Pets { get; } = [];

    public VolunteerSocialNetworks SocialNetworks { get; private set; }

    public VolunteerAssistanceDetails AssistanceDetails { get; private set; }

    public void UpdateMainInfo(Description description, 
        PhoneNumber phoneNumber, 
        ExperienceYear experienceYears,
        FullName fullName)
    {
        Description = description;
        PhoneNumber = phoneNumber;
        ExperienceYear = experienceYears;
        FullName = fullName;
    }

    public void UpdateSocialNetworks(VolunteerSocialNetworks socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }
    
    public void UpdateAssistanceDetail(VolunteerAssistanceDetails assistanceDetail)
    {
        AssistanceDetails = assistanceDetail;
    }

    public void Delete()
    {
        if (_isDeleted) return;
        {
            _isDeleted = true;
            foreach (var pet in _pets)
            {
                pet.Delete();
            }
        }
            
    }

    public void Restore()
    {
        if(_isDeleted)
            _isDeleted = false;
    }
}