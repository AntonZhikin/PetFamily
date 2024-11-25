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
    [Fact]
    public void Add_Pet_With_Empty_Pet_Return_Success_Result()
    {
        //arrange 
        var description = Description.Create("Test Description").Value;
        var phoneNumber = PhoneNumber.Create("+79872724728").Value;
        var experienceYears = ExperienceYear.Create("Test Experience Year").Value;
        var fullname = FullName.Create("Test Name", "Test Last Name", "Test Second Name").Value;

        //подготовка к тесту
        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), description, phoneNumber, experienceYears, fullname,
            null, null);

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
        addedPetResult.Value.SerialNumber.Should().Be(SerialNumber.First);
    }

    [Fact]
    public void Add_Pet_With_Other_Pets_Return_Success_Result()
    {
        const int petCount = 5;
        //arrange 
        var description = Description.Create("Test Description").Value;
        var phonenumber = PhoneNumber.Create("+79872724728").Value;
        var experienceYears = ExperienceYear.Create("Test Experience Year").Value;
        var fullname = FullName.Create("Test Name", "Test Last Name", "Test Second Name").Value;
        var name = Name.Create("Test Name").Value;
        var color = Color.Create("Test Color").Value;

        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), description, phonenumber, experienceYears, fullname,
            null, null);
        var pets = Enumerable.Range(1, 5).Select(_ =>
            new Pet(PetId.NewPetId(), name, color));

        var petId = PetId.NewPetId();

        var petToAdd = new Pet(petId, name, color);

        foreach (var pet in pets)
            volunteer.AddPet(pet);
        
        //act
        
        var result = volunteer.AddPet(petToAdd);
        
        //assert
        var addedPetResult = volunteer.GetPetById(petId);

        var serialNumber = SerialNumber.Create(petCount + 1).Value;
        
        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.Id.Should().Be(petToAdd.Id);
        addedPetResult.Value.SerialNumber.Should().Be(serialNumber);
    }
}