using PetFamily.Domain.Pet.PetID;
using PetFamily.Domain.Pet.PetLists;
using PetFamily.Domain.Pet.PetValueObject;
using PetFamily.Domain.Shared;
namespace PetFamily.Domain.Pet;

public class Pet : Entity<PetId>//, ISoftDeletable
{
    public bool _isDeleted { get; private set; } = false;
    
    //ef core
    private Pet(PetId id) : base(id)
    {
    }

    private Pet(PetId petId, Name name) : base(petId)
    {
        Name = name;
    }
    
    public Name Name { get; private set; }
    
    public Description Description { get; private set; }

    public Color Color { get; private set;}

    public PetHealthInfo PetHealthInfo { get; private set;}

    public Address Address { get; private set;}

    public Weight Weight { get; private set;}

    public Height Height { get; private set;}

    public PhoneNumber PhoneNumber { get; private set;}

    public IsNautered IsNeutered { get; private set;}
    
    public DateOnly DateOfBirth { get; private set;}
    
    public bool IsVaccine { get; private set; }

    public HelpStatus HelpStatus { get; private set;}
    
    public RequisiteList Requisites { get; private set;}
    public DateOnly DateCreate { get; private set;}

    public PetPhotoList Photos { get;}
    public void Delete()
    {
        if (_isDeleted) return;
        {
            _isDeleted = true;
        }
    }

    public void Restore()
    {
        if(_isDeleted)
            _isDeleted = false;
    }
}