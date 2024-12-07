using CSharpFunctionalExtensions;
using PetFamily.Domain.Pet.PetID;
using PetFamily.Domain.Pet.PetLists;
using PetFamily.Domain.Pet.PetValueObject;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Speciess;
using PetFamily.Domain.Speciess.SpeciesID;

namespace PetFamily.Domain.Pet;

public class Pet : Shared.Entity<PetId>//, ISoftDeletable
{
    //ef core
    private Pet(PetId id) : base(id)
    {
    }
    
    public Pet(PetId petId, 
        Name name, 
        Description description, 
        Color color, 
        PetHealthInfo petHealthInfo, 
        Address address,
        Weight weight, 
        Height height, 
        PhoneNumber phoneNumber, 
        IsNautered isNeutered, 
        DateTime dateOfBirth, 
        bool isVaccine,
        HelpStatus helpStatus,
        DateTime dateCreate,
        //BreedId breedId,
        //SpeciesId speciesId,
        RequisiteList requisites,
        IEnumerable<PetPhoto>? photos = null) : base(petId)
    {
        Name = name;
        Description = description;
        Color = color;
        PetHealthInfo = petHealthInfo;
        Address = address;
        Weight = weight;
        Height = height;
        PhoneNumber = phoneNumber;
        IsNeutered = isNeutered;
        DateOfBirth = dateOfBirth;
        IsVaccine = isVaccine;
        HelpStatus = helpStatus;
        DateCreate = dateCreate;
        //BreedId = breedId;
        //SpeciesId = speciesId;
        Requisites = requisites;
        Photos = photos == null 
            ? new PetPhotoList(Enumerable.Empty<PetPhoto>()) 
            : new PetPhotoList(photos);
    }

    //public SpeciesId SpeciesId { get; private set; }

    //public BreedId BreedId { get; private set; }

    public Position Position { get; private set; }
    
    public Name Name { get; private set; }
    
    public Description Description { get; private set;}
    
    public bool _isDeleted { get; private set; } = false;
    
    public Color Color { get; private set;}

    public PetHealthInfo PetHealthInfo { get; private set;}

    public Address Address { get; private set;}

    public Weight Weight { get; private set;}

    public Height Height { get; private set;}

    public PhoneNumber PhoneNumber { get; private set;}

    public IsNautered IsNeutered { get; private set;}
    
    public DateTime DateOfBirth { get; private set;}
    
    public bool IsVaccine { get; private set;}

    public HelpStatus HelpStatus { get; private set;}
    
    public RequisiteList Requisites { get; private set;}
    public DateTime DateCreate { get; private set;}
    public PetPhotoList Photos { get; private set;}

    public void UpdatePhotos(IEnumerable<PetPhoto> photos)
        => Photos = new PetPhotoList(photos); 
    
    
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
    
    public void SetPosition(Position position) =>
        Position = position;

    public UnitResult<Error> MoveForward()
    {
        var newPosition = Position.Forward();
        if(newPosition.IsFailure)
            return newPosition.Error;
        
        Position = newPosition.Value;

        return Result.Success<Error>();
    }
    public UnitResult<Error> MoveBack()
    {
        var newPosition = Position.Back();
        if(newPosition.IsFailure)
            return newPosition.Error;
        
        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public void Move(Position newPosition) =>
        Position = newPosition;
    
}