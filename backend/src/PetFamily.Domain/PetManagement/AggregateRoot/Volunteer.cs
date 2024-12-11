using CSharpFunctionalExtensions;
using PetFamily.Domain.Pet.PetValueObject;
using PetFamily.Domain.PetManagement.Ids;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer.VolunteerID;
using PetFamily.Domain.Volunteer.VolunteerList;
using PetFamily.Domain.Volunteer.VolunteerValueObject;
using Description = PetFamily.Domain.Volunteer.VolunteerValueObject.Description;
using PhoneNumber = PetFamily.Domain.Volunteer.VolunteerValueObject.PhoneNumber;

namespace PetFamily.Domain.PetManagement.AggregateRoot;

public class Volunteer : Shared.Entity<VolunteerId>//, ISoftDeletable
{
    public const int MAX_LENGHT = 100;

    public bool _isDeleted { get; private set; } = false;
    
    //ef core
    private Volunteer(VolunteerId id) : base(id)
    {
        
    }
    
    public Volunteer(
        VolunteerId volunteerId, 
        Description description, 
        PhoneNumber phoneNumber, 
        ExperienceYear experienceYears, 
        FullName fullName,
        SocialNetworkList socialNetworkList,
        AssistanceDetailList assistanceDetailList
        ) 
        : base(volunteerId)
    {
        Description = description;
        PhoneNumber = phoneNumber;
        ExperienceYear = experienceYears;
        FullName = fullName;
        SocialNetworkList = socialNetworkList;
        AssistanceDetailList = assistanceDetailList;
    }

    public FullName FullName { get; private set; }

    public Description Description { get; private set; }

    public ExperienceYear ExperienceYear { get; private set; }
    
    public int GetPetsInHome() => _pets.Count(p => p.HelpStatus == HelpStatus.InHome);
    
    public int GetPetFoundHome() => _pets.Count(p => p.HelpStatus == HelpStatus.FoundHome);

    public int GetPetHealing() => _pets.Count(p => p.HelpStatus == HelpStatus.PetHealing);
    
    public UnitResult<Error> DeletePetPhotos(PetId petId)
    {
        var pet = Pets.FirstOrDefault(p => p.Id == petId);
        if (pet is null)
            return Errors.General.NotFound();

        pet.DeleteAllPhotos();

        return Result.Success<Error>();
    }

    public Result<Entity.Pet, Error> GetPetById(PetId petId)
    { 
        var pet = _pets.FirstOrDefault(p => p.Id.Value == petId.Value);
        if (pet is null)
            return Errors.General.NotFound(petId.Value);

        return pet;
    }
    
    public PhoneNumber PhoneNumber;
    
    private List<Entity.Pet> _pets = [];

    public IReadOnlyList<Entity.Pet> Pets => _pets;

    public SocialNetworkList SocialNetworkList { get; private set; }

    public AssistanceDetailList AssistanceDetailList { get; private set; }

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

    public void UpdateSocialNetworks(SocialNetworkList socialNetworkList)
    {
        SocialNetworkList = socialNetworkList;
    }
    
    public void UpdateAssistanceDetail(AssistanceDetailList assistanceDetailList)
    {
        AssistanceDetailList = assistanceDetailList;
    }

    public void SoftDelete()
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
    
    public UnitResult<Error> AddPet(Entity.Pet pet)
    {
        var serialNumberResult = Position.Create(_pets.Count + 1);
        if (serialNumberResult.IsFailure)
            return serialNumberResult.Error;
        
        
        pet.SetPosition(serialNumberResult.Value);
        
        _pets.Add(pet);
        
        return Result.Success<Error>();
    }
    
    public UnitResult<Error> MovePet(Entity.Pet pet, Position newPosition)
    {
        var currentPosition = pet.Position;
        
        if (currentPosition == newPosition || _pets.Count() == 1)
            return Result.Success<Error>();

        var adjustedPosition = AdjustNewPositionIfOutOfRange(newPosition);
        if(adjustedPosition.IsFailure)
            return adjustedPosition.Error;
        
        newPosition = adjustedPosition.Value;

        var moveResult = MovePetBetweenPositions(newPosition, currentPosition);
        if(moveResult.IsFailure)
            return moveResult.Error;

        pet.Move(newPosition);
        
        return Result.Success<Error>();
    }

    private UnitResult<Error> MovePetBetweenPositions(Position newPosition, Position currentPosition)
    {
        if (newPosition < currentPosition)
        {
            var petsToMove = _pets.Where(i => i.Position >= newPosition
                                              && i.Position <= currentPosition);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveForward();
                if (result.IsFailure)
                {
                    return result.Error;
                }
            }
        }
        else if (newPosition > currentPosition)
        {
            var petsToMove = _pets.Where(i => i.Position > currentPosition
                                              && i.Position <= newPosition);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveBack();
                if (result.IsFailure)
                {
                    return result.Error;
                }
            }
        }
        
        

        return Result.Success<Error>();
    }

    private Result<Position, Error> AdjustNewPositionIfOutOfRange(Position newPosition)
    {
        if(newPosition.Value <= _pets.Count())
            return newPosition;
        var lastPosition = Position.Create(_pets.Count - 1);
        if (lastPosition.IsFailure)
            return lastPosition.Error;  
        
        return lastPosition.Value;
    } 
}