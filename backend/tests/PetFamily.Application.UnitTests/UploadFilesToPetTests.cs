using System.Collections;
using System.Data;
using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using PetFamily.Application.Database;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Volunteers;
using PetFamily.Application.Volunteers.DTOs;
using PetFamily.Application.Volunteers.UploadFilesToPet;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Pet.PetValueObject;
using PetFamily.Domain.PetManagement.AggregateRoot;
using PetFamily.Domain.PetManagement.Entity;
using PetFamily.Domain.PetManagement.Ids;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Ids;
using PetFamily.Domain.Speciess.SpeciesID;
using PetFamily.Domain.Volunteer.VolunteerID;
using PetFamily.Domain.Volunteer.VolunteerValueObject;
using IFileProvider = PetFamily.Application.FileProvider.IFileProvider;

namespace Application.UnitTests;

public class UploadFilesToPetTests
{
    [Fact]
    public async Task Handle_Should_Upload_Files_To_Pet()
    {
        // arrange
        var cancellationToken = new CancellationTokenSource().Token;

        var (volunteer, pet) = CreateVolunteerAndPet();
        
        var stream = new MemoryStream();
        var filename = "test.jpg";

        var uploadFileDto = new UploadFileDto(stream, filename);
        List<UploadFileDto> files = [uploadFileDto, uploadFileDto];
        
        var command = new UploadFileToPetCommand(volunteer.Id.Value, pet.Id.Value, files);
        
        
        //mock
        var fileProviderMock = new Mock<IFileProvider>();

        List<PhotoPath> photoPaths =
        [
            PhotoPath.Create(filename).Value,
            PhotoPath.Create(filename).Value,
        ];

        fileProviderMock
            .Setup(v => v.UploadFiles(It.IsAny<List<FileData>>(), cancellationToken))
            .ReturnsAsync(Result.Success<IReadOnlyList<PhotoPath>, Error>(photoPaths));
        
        var volunteerRepositoryMock = new Mock<IVolunteerRepository>();

        volunteerRepositoryMock
            .Setup(m => m.GetById(volunteer.Id, cancellationToken))
            .ReturnsAsync(volunteer);
        
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock
            .Setup(u => u.SaveChanges(cancellationToken))
            .Returns(Task.CompletedTask);
        
        var validatorMock = new Mock<IValidator<UploadFileToPetCommand>>();

        validatorMock
            .Setup(v => v.ValidateAsync(
                It.IsAny<UploadFileToPetCommand>(),
                cancellationToken))
            .ReturnsAsync(new ValidationResult());
        
        var loggerMock = new Mock<ILogger<UploadFileToPetHandler>>();
        
        var handler = new UploadFileToPetHandler(
            fileProviderMock.Object,
            validatorMock.Object,
            volunteerRepositoryMock.Object,
            loggerMock.Object,
            unitOfWorkMock.Object
            );
        
        // act
        var result = await handler.Handle(command, cancellationToken);
        
        // assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(pet.Id.Value);
    }
    
    

    private (Volunteer volunteer, Pet pet) CreateVolunteerAndPet()
    {
        var description = Description.Create("A description").Value;
        var phoneNumber = PhoneNumber.Create("+79872724728").Value;
        var experienceYear = ExperienceYear.Create("A ExperienceYear").Value;
        var fullName = FullName.Create("name", "lastname", "firstname").Value;

        var name = Name.Create("command.Name").Value;
        var color = Color.Create("command.Color").Value;
        var petHealthInfo = PetHealthInfo.Create("command.PetHealthInfo").Value;
        var address = Address.Create("command.City", "command.Street").Value;
        var weight = Weight.Create(1).Value;
        var height = Height.Create(1).Value;
        var isNeutered = IsNautered.Create(true).Value;
        var dateOfBirth = DateTime.Now;
        var isVaccine = true;
        var helpStatus = HelpStatus.FoundHome;
        var dateCreated = DateTime.Now;
        var speciesId = SpeciesId.Empty();
        var breedId = BreedId.Empty();
        var requisites = Requisite.Create("Name", "Description").Value;
        var petId = PetId.NewPetId();

        var volunteer = new Volunteer(
            VolunteerId.NewVolunteerId(),
            description,
            phoneNumber,
            experienceYear,
            fullName,
            null,
            null);

        var pet = new Pet(
            petId,
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
            dateCreated,
            null,
            null);

        volunteer.AddPet(pet);

        return (volunteer, pet);
    }
}