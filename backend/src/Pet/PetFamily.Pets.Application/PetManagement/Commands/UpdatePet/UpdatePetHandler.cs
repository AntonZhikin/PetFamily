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
using PetFamily.Pets.Domain.ValueObjects;
using PetFamily.Species.Contracts;
using PetFamily.Species.Contracts.Request;

namespace PetFamily.Pets.Application.PetManagement.Commands.UpdatePet;

public class UpdatePetHandler : ICommandHandler<Guid, UpdatePetCommand>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<UpdatePetHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePetCommand> _validator;
    private readonly ISpecieContract _specieContract;

    public UpdatePetHandler(
        IReadDbContext readDbContext,
        ILogger<UpdatePetHandler> logger,
        IVolunteerRepository volunteerRepository,
        [FromKeyedServices(Modules.Pets)]IUnitOfWork unitOfWork,
        IValidator<UpdatePetCommand> validator,
        ISpecieContract specieContract)
    {
        _readDbContext = readDbContext;
        _logger = logger;
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _specieContract = specieContract;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(UpdatePetCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

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
        
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var speciesBreed = SpeciesDetails.Create(speciesId, breedId);
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
        
        var petToUpdate = volunteerResult.Value.Pets.FirstOrDefault(p => p.Id == command.PetId);
        if (petToUpdate == null)
            return Errors.General.NotFound().ToErrorList();
        
        petToUpdate.UpdateMainInfo(
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
            speciesBreed.Value,
            requisites);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("For pet with Id: {id} was updated main info", petToUpdate.Id);

        return volunteerResult.Value.Id.Value;
    }
}