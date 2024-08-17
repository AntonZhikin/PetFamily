using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet;

public sealed class Pet : Entity<PetId>
{
    //ef core
    private Pet(PetId id) : base(id)
    {
    }

    private Pet(PetId petId, NameValue name) : base(petId)
    {
        Name = name;
    }
    
    public NameValue Name;
    
    public SpeciesValue Species;
    
    public DescriptionValue Description;
    
    public BreedValue Breed;

    public ColorValue Color;

    public PetHealthInfoValue PetHealthInfo;

    public AddressValue Address;

    public WeightValue Weight;

    public HeightValue Height;

    public PhoneNumberValue PhoneNumber;

    public IsNauteredValue IsNeutered;
    
    public DateOnly DateOfBirth { get; private set; }
    
    public bool IsVaccine { get; private set; }

    public InfoValue Status { get; private set; }
    
    // public enum Info
    // {
    //     NeedsHelp,
    //     LookingForHome,
    //     FoundHome
    // }
    
    public ReqList ReqDetails { get; private set; }
    
    public DateOnly DateCreate { get; private set; }

    public PetList Details { get; private set; }
}