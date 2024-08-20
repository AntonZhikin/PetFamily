using PetFamily.Domain.Shared;
namespace PetFamily.Domain.Pet;

public sealed class Pet : Entity<PetId>
{
    //ef core
    private Pet(PetId id) : base(id)
    {
    }

    private Pet(PetId petId, Name name) : base(petId)
    {
        Name = name;
    }
    
    public Name Name;
    
    //public SpeciesValue Species;
    
    public Description Description;
    
    //public BreedValue Breed;

    public Color Color;

    public PetHealthInfo PetHealthInfo;

    public Address Address;

    public Weight Weight;

    public Height Height;

    public PhoneNumber PhoneNumber;

    public IsNautered IsNeutered;
    
    public DateOnly DateOfBirth { get; private set; }
    
    public bool IsVaccine { get; private set; }

    public HelpStatus HelpStatus { get; private set; }
    
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