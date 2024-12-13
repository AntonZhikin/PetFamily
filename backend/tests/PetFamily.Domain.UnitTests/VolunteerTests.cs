using FluentAssertions;
using PetFamily.Domain.PetManagement.AggregateRoot;
using PetFamily.Domain.PetManagement.Entity;
using PetFamily.Domain.PetManagement.Ids;
using PetFamily.Domain.PetManagement.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Ids;
using Description = PetFamily.Domain.PetManagement.ValueObjects.Description;
using PhoneNumber = PetFamily.Domain.PetManagement.ValueObjects.PhoneNumber;

namespace PetFamily.UnitTests;

public class VolunteerTests
{
    
    //Юниты проверить
    /*[Fact]
    public void Add_Pet_With_Empty_Pet_Return_Success_Result()
    {
        //arrange 
        var volunteer = CreateVolunteerWithPets(0);

        var name = Name.Create("command.Name").Value;
        var description = Description.Create("command.Description").Value;
        var color = Color.Create("command.Color").Value;
        var petHealthInfo = PetHealthInfo.Create("command.PetHealthInfo").Value;
        var address = Address.Create("command.City", "command.Street").Value;
        var weight = Weight.Create(1).Value;
        var height = Height.Create(1).Value;
        var phoneNumber = PhoneNumber.Create("command.PhoneNumber").Value;
        var isNeutered = IsNautered.Create(true).Value;
        var dateOfBirth = "command.DateOfBirth";
        var isVaccine = true;
        var helpStatus = "command.HelpStatus";
        var dateCreate = "command.DateCreate";
        var speciesId = SpeciesId.Empty();
        var breedId = BreedId.Empty();
        //var photos = command.Files
        //    .Select(f => PetPhoto.Create(f.Path, f.IsMain).Value);
        var requisites = Requisite.Create("Name", "Description").Value;
        var petId = PetId.NewPetId();
        var petToAdd = new Pet(petId, 
            name, 
            description, 
            color, 
            petHealthInfo, 
            address, 
            weight, 
            height, 
            phoneNumber, 
            isNeutered, 
            dateOfBirth, 
            isVaccine,
            helpStatus, 
            dateCreate,
            breedId,
            speciesId,
            requisites);

        //act
        //вызов тестриуемого метода
        var result = volunteer.AddPet(petToAdd);


        // assert
        var addedPetResult = volunteer.GetPetById(petId);

        //проверка результата
        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.Id.Should().Be(petToAdd.Id);
        addedPetResult.Value.Position.Should().Be(Position.First);
    }

    [Fact]
    public void Add_Pet_With_Other_Pets_Return_Success_Result()
    {
        //arrange
        const int petCount = 5;
        
        var volunteer = CreateVolunteerWithPets(petCount);
        
        var name = Name.Create("command.Name").Value;
        var description = Description.Create("command.Description").Value;
        var color = Color.Create("command.Color").Value;
        var petHealthInfo = PetHealthInfo.Create("command.PetHealthInfo").Value;
        var address = Address.Create("command.City", "command.Street").Value;
        var weight = Weight.Create(1).Value;
        var height = Height.Create(1).Value;
        var phoneNumber = PhoneNumber.Create("command.PhoneNumber").Value;
        var isNeutered = IsNautered.Create(true).Value;
        var dateOfBirth = "command.DateOfBirth";
        var isVaccine = true;
        var helpStatus = "command.HelpStatus";
        var dateCreate = "command.DateCreate";
        var speciesId = SpeciesId.Empty();
        var breedId = BreedId.Empty();
        //var photos = command.Files
        //    .Select(f => PetPhoto.Create(f.Path, f.IsMain).Value);
        var requisites = Requisite.Create("Name", "Description").Value;
        var petId = PetId.NewPetId();
        
        var petToAdd = new Pet(petId, 
            name, 
            description, 
            color, 
            petHealthInfo, 
            address, 
            weight, 
            height, 
            phoneNumber, 
            isNeutered, 
            dateOfBirth, 
            isVaccine,
            helpStatus, 
            dateCreate,
            breedId,
            speciesId,
            requisites);
        
        //act
        
        var result = volunteer.AddPet(petToAdd);
        
        //assert
        var addedPetResult = volunteer.GetPetById(petId);

        var serialNumber = Position.Create(petCount + 1).Value;
        
        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.Id.Should().Be(petToAdd.Id);
        addedPetResult.Value.Position.Should().Be(serialNumber);
    }*/

    [Fact]
    public void Move_Pet_Should_Not_Move_When_Pet_Already_At_New_Position()
    {
        const int petCount = 6;
        
        // arrange
        var volunteer = CreateVolunteerWithPets(petCount);
        
        var secondPosition = Position.Create(2).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        
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
        var phonenumber = PhoneNumber.Create("+79872724728").Value;
        var experienceYears = ExperienceYear.Create("Test Experience Year").Value;
        var fullname = FullName.Create("Test Name", "Test Last Name", "Test Second Name").Value;
        var name = Name.Create("command.Name").Value;
        var description = Description.Create("command.Description").Value;
        var color = Color.Create("command.Color").Value;
        var petHealthInfo = PetHealthInfo.Create("command.PetHealthInfo").Value;
        var address = Address.Create("command.City", "command.Street").Value;
        var weight = Weight.Create(1).Value;
        var height = Height.Create(1).Value;
        var isNeutered = IsNautered.Create(true).Value;
        DateTime dateOfBirth = DateTime.Now;
        var isVaccine = true;
        var helpStatus = HelpStatus.FoundHome;
        DateTime dateCreate = DateTime.Now;
        var speciesId = SpeciesId.Empty();
        var breedId = BreedId.Empty();
        //var photos = command.Files
        //    .Select(f => PetPhoto.Create(f.Path, f.IsMain).Value);
        var requisites = Requisite.Create("Name", "Description").Value;
        var petId = PetId.NewPetId();
        
        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), description, phonenumber, experienceYears, fullname,
            null, null);

        for (var i = 0; i < petCount; i++)
        {
            var pet = new Pet(petId, 
                name, 
                description, 
                color, 
                petHealthInfo, 
                address, 
                weight, 
                height, 
                phonenumber, 
                isNeutered, 
                dateOfBirth, 
                isVaccine,
                helpStatus, 
                dateCreate,
                null);
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

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        
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

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        
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

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        
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

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        
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