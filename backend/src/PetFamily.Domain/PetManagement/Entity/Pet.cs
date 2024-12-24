using CSharpFunctionalExtensions;
using PetFamily.Domain.PetManagement.Ids;
using PetFamily.Domain.PetManagement.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Domain.PetManagement.Entity;

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
        SpeciesDetails speciesDetails,
        RequisiteList requisites,
        ValueObjectList<PetPhoto>? photos = null) : base(petId)
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
        SpeciesDetails = speciesDetails;
        Requisites = requisites;
        _photos = photos ?? new ValueObjectList<PetPhoto>([]);
    }
    
    public SpeciesDetails SpeciesDetails { get; private set; }

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
    public IReadOnlyList<PetPhoto> Photos => _photos;

    private List<PetPhoto> _photos = [];
    
    public void UpdatePhotos(List<PetPhoto> photos)
    {
        _photos = photos;
    }

    public void DeleteAllPhotos() => _photos = [];
    
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

    public void UpdateMainInfo(
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
        SpeciesDetails speciesDetails,
        RequisiteList requisites)
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
        SpeciesDetails = speciesDetails;
        Requisites = requisites;
    }

    public void UpdateMainPhoto(PhotoPath photoPath)
    {
        var newMainPhoto = Photos.FirstOrDefault(p => p.PathToStorage == photoPath.Path);
        if (newMainPhoto != null) 
            newMainPhoto.IsMain = true;
    }
    
    public void UpdateStatus(HelpStatus newStatus)
    {
        HelpStatus = newStatus;
    }
}