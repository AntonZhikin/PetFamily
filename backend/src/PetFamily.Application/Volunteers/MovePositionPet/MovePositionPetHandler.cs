using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Species;
using PetFamily.Domain.Pet.PetValueObject;
using PetFamily.Domain.PetManagement.AggregateRoot;
using PetFamily.Domain.PetManagement.Ids;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.MovePositionPet;

public class MovePositionPetHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MovePositionPetHandler> _logger;
    private readonly IValidator<MovePositionPetCommand> _validator;

    public MovePositionPetHandler(
        IUnitOfWork unitOfWork,
        IValidator<MovePositionPetCommand> validator,
        ILogger<MovePositionPetHandler> logger,
        IVolunteerRepository volunteerRepository,
        ISpeciesRepository speciesRepository
        )
    {
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
        _speciesRepository = speciesRepository;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        MovePositionPetCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await  _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var volunteerResult = await _volunteerRepository
            .GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var petId = PetId.Create(command.PetId);
        var petResult = volunteerResult.Value.GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        var newPetPosition = Position.Create(command.NewPosition);
        
        volunteerResult.Value.MovePet(petResult.Value, newPetPosition.Value);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Pet - {petId} moved to position {newPosition}",
            petResult.Value.Id.Value, command.NewPosition);
        
        return petResult.Value.Id.Value;
    }
    
}