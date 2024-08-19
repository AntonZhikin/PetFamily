using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer;
using PetFamily.Domain.Pet;

public class Volunteer : Entity<VolunteerId>
{
    private Volunteer(VolunteerId id) : base(id)
    {
        
    }
    
    private Volunteer(VolunteerId volunteerId, string descriptions) : base(volunteerId)
    {
        Descriptions = descriptions;
    }
    
    //public Guid Id { get; private set; }

    public FullName FullName;

    public string Descriptions { get; private set; } = null!;

    public int ExperienceYears { get; private set; }

    public int CountPetInHome { get; private set; }

    public int CountPetFoundHome { get; private set; }

    public int CountPetHealing { get; private set; }

    public string PhoneNumber { get; private set; } = null!;

    public List<Pet> Pets { get; private set; } = [];

    public VoluunterSocialList SocDetails;

    public VoluunterReqList ReqListDetails;
}