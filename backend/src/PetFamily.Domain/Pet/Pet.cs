using PetFamily.Domain.Pet.PetID;
using PetFamily.Domain.Pet.PetList;
using PetFamily.Domain.Pet.PetValueObject;
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
    
    public DateOnly DateOfBirth { get; }
    
    public bool IsVaccine { get; }

    public HelpStatus HelpStatus { get;}
    
    public ReqList ReqDetails { get;}
    
    public DateOnly DateCreate { get; }

    public PetList.PetList PetDetails { get; }
}