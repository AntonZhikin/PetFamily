using CSharpFunctionalExtensions;
using PetFamily.Core.DTOs.ValueObject;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.Pets.Domain.Entity;
using PetFamily.Pets.Domain.ValueObjects;
using Description = PetFamily.Pets.Domain.ValueObjects.Description;
using PhoneNumber = PetFamily.Pets.Domain.ValueObjects.PhoneNumber;

namespace PetFamily.Pets.Domain.AggregateRoot;

public class Volunteer : SoftDeletableEntity<VolunteerId>
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
        FullName fullName
    )
        : base(volunteerId)
    {
        Description = description;
        PhoneNumber = phoneNumber;
        FullName = fullName;
    }

    public FullName FullName { get; private set; }

    public Description Description { get; private set; }
    
    private List<Pet> _pets = [];

    public IReadOnlyList<Pet> Pets => _pets;

    public int GetPetsInHome() => _pets.Count(p => p.HelpStatus == HelpStatus.InHome);

    public int GetPetFoundHome() => _pets.Count(p => p.HelpStatus == HelpStatus.FoundHome);

    public int GetPetHealing() => _pets.Count(p => p.HelpStatus == HelpStatus.PetHealing);

    public PhoneNumber PhoneNumber;
    
    public override void Delete()
    {
        base.Delete();

        foreach (var pet in _pets)
        {
            pet.Delete();
        }
    }
    
    public void UpdateMainInfo(Description description,
        PhoneNumber phoneNumber,
        FullName fullName)
    {
        Description = description;
        PhoneNumber = phoneNumber;
        FullName = fullName;
    }

    public UnitResult<Error> DeletePetPhotos(PetId petId)
    {
        var pet = GetPetById(petId);

        pet.Value.DeleteAllPhotos();

        return Result.Success<Error>();
    }

    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId);
        if (pet is null)
            return Errors.General.NotFound(petId.Value);

        return pet;
    }

    public UnitResult<Error> UpdatePetPosition(List<Pet> orderedList)
    {
        for (var i = 0; i < _pets.Count; i++)
        {
            var pet = orderedList[i];

            var positionNumber = Position.Create(i + 1);
            if (positionNumber.IsFailure)
                return Errors.General.ValueIsInvalid("positionNumber");

            pet.SetPosition(positionNumber.Value);
        }

        return Result.Success<Error>();
    }


    public UnitResult<Error> AddPet(Pet pet)
    {
        var serialNumberResult = Position.Create(_pets.Count + 1);
        if (serialNumberResult.IsFailure)
            return serialNumberResult.Error;


        pet.SetPosition(serialNumberResult.Value);

        _pets.Add(pet);

        return Result.Success<Error>();
    }
    
    public UnitResult<Error> MovePet(Pet pet, Position newPosition)
    {
        var currentPosition = pet.Position;

        if (currentPosition == newPosition || _pets.Count() == 1)
            return Result.Success<Error>();

        var adjustedPosition = AdjustNewPositionIfOutOfRange(newPosition);
        if (adjustedPosition.IsFailure)
            return adjustedPosition.Error;

        newPosition = adjustedPosition.Value;

        var moveResult = MovePetBetweenPositions(newPosition, currentPosition);
        if (moveResult.IsFailure)
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
        if (newPosition.Value <= _pets.Count())
            return newPosition;
        var lastPosition = Position.Create(_pets.Count - 1);
        if (lastPosition.IsFailure)
            return lastPosition.Error;

        return lastPosition.Value;
    }

    public void DeleteExpiredPets(int daysBeforeDelete)
    {
        _pets.RemoveAll(p => p.DeletionDate != null 
                             && DateTime.UtcNow > p.DeletionDate.Value
                                 .AddDays(daysBeforeDelete));
    }
    public void HardDeletePet(Pet pet)
    {
        _pets.Remove(pet);
        foreach (var p in _pets.Where(p => p.Position.Value > pet.Position.Value))
        {
            var newPosition = (Position.Create(p.Position.Value - 1).Value);
            p.SetPosition(newPosition);
        }
    }
}