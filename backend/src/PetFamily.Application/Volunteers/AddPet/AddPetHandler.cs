using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Pet.PetID;
using PetFamily.Domain.Pet.PetLists;
using PetFamily.Domain.Pet.PetValueObject;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Speciess.SpeciesID;
using PetFamily.Domain.Volunteer.VolunteerID;

namespace PetFamily.Application.Volunteers.AddPet;

public class AddPetHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPetHandler> _logger;


    public AddPetHandler(
        IValidator<AddPetCommand> validator,
        IVolunteerRepository volunteerRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddPetHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var volunteerResult = await _volunteerRepository
            .GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petId = PetId.NewPetId();

        var name = Name.Create(command.Name).Value;
        var description = Description.Create(command.Description).Value;
        var color = Color.Create(command.Color).Value;
        var petHealthInfo = PetHealthInfo.Create(command.PetHealthInfo).Value;
        var address = Address.Create(command.City, command.Street).Value;
        var weight = Weight.Create(command.Weight).Value;
        var height = Height.Create(command.Height).Value;
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        var isNeutered = IsNautered.Create(command.IsNeutered).Value;
        var dateOfBirth = command.DateOfBirth;
        var isVaccine = command.IsVaccine;
        var helpStatus = command.HelpStatus;
        var dateCreate = command.DateCreate;
        var speciesId = SpeciesId.Empty();
        var breedId = BreedId.Empty();

        var requisites = new RequisiteList(
            command.RequisiteList.Requisites
                .Select(r =>
                    Requisite.Create(r.Name, r.Description).Value));

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
            dateCreate,
            requisites,
            null
        );

        volunteerResult.Value.AddPet(pet);
        
        _volunteerRepository.Save(volunteerResult.Value);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Pet added with id: {PetId}.", pet.Id.Value);

        return pet.Id.Value;
    }
}