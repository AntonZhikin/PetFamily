using FluentAssertions;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Pet.PetID;
using PetFamily.Domain.Pet.PetValueObject;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.VolunteerID;
using PetFamily.Domain.Volunteer.VolunteerValueObject;
using Description = PetFamily.Domain.Volunteer.VolunteerValueObject.Description;
using PhoneNumber = PetFamily.Domain.Volunteer.VolunteerValueObject.PhoneNumber;

namespace PetFamily.UnitTests;

public class VolunteerTests
{
    
    //Юниты проверить
    [Fact]
    public void Add_Pet_With_Empty_Pet_Return_Success_Result()
    {
        //arrange 
        var volunteer = CreateVolunteerWithPets(0);

        var name = Name.Create("TestName").Value;
        var color = Color.Create("TestColor").Value;
        var petId = PetId.NewPetId();
        var pet = new Pet(petId, name, color);


        //act
        //вызов тестриуемого метода
        var result = volunteer.AddPet(pet);


        // assert
        var addedPetResult = volunteer.GetPetById(petId);

        //проверка результата
        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.Id.Should().Be(pet.Id);
        addedPetResult.Value.Position.Should().Be(Position.First);
    }

    [Fact]
    public void Add_Pet_With_Other_Pets_Return_Success_Result()
    {
        //arrange
        const int petCount = 5;
        
        var volunteer = CreateVolunteerWithPets(petCount);
        
        var petId = PetId.NewPetId(); 
        
        var name = Name.Create("TestName").Value;
        var color = Color.Create("TestColor").Value;
        
        var petToAdd = new Pet(petId, name, color);
        
        //act
        
        var result = volunteer.AddPet(petToAdd);
        
        //assert
        var addedPetResult = volunteer.GetPetById(petId);

        var serialNumber = Position.Create(petCount + 1).Value;
        
        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.Id.Should().Be(petToAdd.Id);
        addedPetResult.Value.Position.Should().Be(serialNumber);
    }

    [Fact]

    public void Move_Pet_Should_Not_Move_When_Pet_Already_At_New_Position()
    {
        const int petCount = 6;
        
        // arrange
        var volunteer = CreateVolunteerWithPets(petCount);
        
        var secondPosition = Position.Create(2).Value;

        var firstPet = volunteer.Pet[0];
        var secondPet = volunteer.Pet[1];
        var thirdPet = volunteer.Pet[2];
        var fourthPet = volunteer.Pet[3];
        var fifthPet = volunteer.Pet[4];
        
        
        // act
        var result = volunteer.MovePet(secondPet, secondPosition);

        //assert
        result.IsSuccess.Should().BeTrue(); 
        //проверить позицию задачи
        firstPet.Position.Should().Be(Position.Create(1).Value);
        secondPet.Position.Should().Be(Position.Create(2).Value);
        thirdPet .Position.Should().Be(Position.Create(3).Value);
        fourthPet .Position.Should().Be(Position.Create(4).Value);
        fifthPet .Position.Should().Be(Position.Create(5).Value );


    }

    private Volunteer CreateVolunteerWithPets(int petCount = 5)
    {
        var description = Description.Create("Test Description").Value;
        var phonenumber = PhoneNumber.Create("+79872724728").Value;
        var experienceYears = ExperienceYear.Create("Test Experience Year").Value;
        var fullname = FullName.Create("Test Name", "Test Last Name", "Test Second Name").Value;
        var name = Name.Create("Test Name").Value;
        var color = Color.Create("Test Color").Value;
        
        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), description, phonenumber, experienceYears, fullname,
            null, null);

        for (var i = 0; i < petCount; i++)
        {
            var pet = new Pet(PetId.NewPetId(), name, color);
            volunteer.AddPet(pet);
        }
        
        return volunteer;
    }
    
    [Fact]
    public void Move_Pet_Should_Move_Others_Pets_Forward_When_New_Position_Is_Lower()
    {
        const int petCount = 6;
        
        // arrange
        var volunteer = CreateVolunteerWithPets(petCount);
        
        var secondPosition = Position.Create(2).Value;

        var firstPet = volunteer.Pet[0];
        var secondPet = volunteer.Pet[1];
        var thirdPet = volunteer.Pet[2];
        var fourthPet = volunteer.Pet[3];
        var fifthPet = volunteer.Pet[4];
        
        
        // act
        var result = volunteer.MovePet(fourthPet, secondPosition);

        //assert
        result.IsSuccess.Should().BeTrue(); 
        //проверить позицию задачи
        firstPet.Position.Should().Be(Position.Create(1).Value);
        secondPet.Position.Should().Be(Position.Create(3).Value);
        thirdPet .Position.Should().Be(Position.Create(4).Value);
        fourthPet .Position.Should().Be(Position.Create(2).Value);
        fifthPet .Position.Should().Be(Position.Create(5).Value );
    }
    
    [Fact]
    public void Move_Pet_Should_Move_Others_Pets_Back_When_New_Position_Is_Grater()
    {
        const int petCount = 6;
        
        // arrange
        var volunteer = CreateVolunteerWithPets(petCount);
        
        var fourthPosition = Position.Create(4).Value;

        var firstPet = volunteer.Pet[0];
        var secondPet = volunteer.Pet[1];
        var thirdPet = volunteer.Pet[2];
        var fourthPet = volunteer.Pet[3];
        var fifthPet = volunteer.Pet[4];
        
        
        // act
        var result = volunteer.MovePet(secondPet, fourthPosition);

        //assert
        result.IsSuccess.Should().BeTrue(); 
        //проверить позицию задачи
        firstPet.Position.Should().Be(Position.Create(1).Value);
        secondPet.Position.Should().Be(Position.Create(4).Value);
        thirdPet .Position.Should().Be(Position.Create(2).Value);
        fourthPet .Position.Should().Be(Position.Create(3).Value);
        fifthPet .Position.Should().Be(Position.Create(5).Value ); 
    }
    
    [Fact]
    public void Move_Pet_Should_Move_Others_Pets_Forward_When_New_Position_Is_First()
    {
        const int petCount = 6;
        
        // arrange
        var volunteer = CreateVolunteerWithPets(petCount);
        
        var firstPosition = Position.Create(1).Value;

        var firstPet = volunteer.Pet[0];
        var secondPet = volunteer.Pet[1];
        var thirdPet = volunteer.Pet[2];
        var fourthPet = volunteer.Pet[3];
        var fifthPet = volunteer.Pet[4];
        
        
        // act
        var result = volunteer.MovePet(fifthPet, firstPosition);

        //assert
        result.IsSuccess.Should().BeTrue(); 
        //проверить позицию задачи
        firstPet.Position.Should().Be(Position.Create(2).Value);
        secondPet.Position.Should().Be(Position.Create(3).Value);
        thirdPet .Position.Should().Be(Position.Create(4).Value);
        fourthPet .Position.Should().Be(Position.Create(5).Value);
        fifthPet .Position.Should().Be(Position.Create(1).Value ); 
    }
    
    [Fact]
    public void Move_Pet_Should_Move_Others_Pets_Back_When_New_Position_Is_Last()
    {
        const int petCount = 6;
        
        // arrange
        var volunteer = CreateVolunteerWithPets(petCount);
        
        var fifthPosition = Position.Create(5).Value;

        var firstPet = volunteer.Pet[0];
        var secondPet = volunteer.Pet[1];
        var thirdPet = volunteer.Pet[2];
        var fourthPet = volunteer.Pet[3];
        var fifthPet = volunteer.Pet[4];
        
        
        // act
        var result = volunteer.MovePet(firstPet, fifthPosition);

        //assert
        result.IsSuccess.Should().BeTrue(); 
        //проверить позицию задачи
        firstPet.Position.Should().Be(Position.Create(5).Value);
        secondPet.Position.Should().Be(Position.Create(1).Value);
        thirdPet .Position.Should().Be(Position.Create(2).Value);
        fourthPet .Position.Should().Be(Position.Create(3).Value);
        fifthPet .Position.Should().Be(Position.Create(4).Value ); 
    }
}