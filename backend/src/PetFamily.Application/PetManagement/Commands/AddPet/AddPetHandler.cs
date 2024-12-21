using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Species;
using PetFamily.Domain.PetManagement.Entity;
using PetFamily.Domain.PetManagement.Ids;
using PetFamily.Domain.PetManagement.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;
using PetFamily.Domain.SpeciesManagement.Ids;

namespace PetFamily.Application.PetManagement.Commands.AddPet;

public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IReadDbContext _readDbContext;


    public AddPetHandler(
        IValidator<AddPetCommand> validator,
        IVolunteerRepository volunteerRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddPetHandler> logger,
        IReadDbContext readDbContext)
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _readDbContext = readDbContext;
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


        var speciesQuery = _readDbContext.Species;
        var speciesDto = await speciesQuery
            .SingleOrDefaultAsync(s => s.Id == command.SpeciesId, cancellationToken);
        if (speciesDto == null)
            return Errors.General.NotFound().ToErrorList();

        var speciesId = speciesDto.Id;

        var breedQuery = _readDbContext.Breed;
        var breedDto = await breedQuery
            .SingleOrDefaultAsync(b => b.Id == command.BreedId, cancellationToken);
        if (breedDto == null)
            return Errors.General.NotFound().ToErrorList();

        var breedId = breedDto.Id;
        
        var speciesDetails = SpeciesDetails.Create(speciesId, breedId);
        
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
            speciesDetails.Value,
            requisites
        );

        volunteerResult.Value.AddPet(pet);
        
        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Pet added with id: {PetId}.", pet.Id.Value);

        return pet.Id.Value;
    }
}