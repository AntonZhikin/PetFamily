using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.Pets.Domain.Entity;
using PetFamily.Pets.Domain.ValueObjects;
using PetFamily.Species.Contracts;
using PetFamily.Species.Contracts.Request;

namespace PetFamily.Pets.Application.PetManagement.Commands.AddPet;

public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly ISpecieContract _specieContract;


    public AddPetHandler(
        IValidator<AddPetCommand> validator,
        IVolunteerRepository volunteerRepository,
        [FromKeyedServices(Modules.Pets)]IUnitOfWork unitOfWork,
        ILogger<AddPetHandler> logger,
        IReadDbContext readDbContext, ISpecieContract specieContract)
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _readDbContext = readDbContext;
        _specieContract = specieContract;
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
        
        var speciesDto = await _specieContract
            .GetSpeciesById(new GetSpecieByIdRequest(command.SpeciesId), cancellationToken);
        if (speciesDto == null)
            return Errors.General.NotFound().ToErrorList();
        
        var speciesId = speciesDto.Id;

        var breedDto = await _specieContract
            .GetBreedById(new GetBreedByIdRequest(command.SpeciesId, command.BreedId), cancellationToken);
        if (breedDto == null)
            return Errors.General.NotFound().ToErrorList();

        var breedId = breedDto.Id;
        
        var speciesDetails = SpeciesDetails
            .Create(SpeciesId.Create(speciesId), BreedId.Create(breedId));
        
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