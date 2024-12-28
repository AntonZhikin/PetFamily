using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.Pets.Domain.ValueObjects;

namespace PetFamily.Pets.Application.PetManagement.Commands.MovePositionPet;

public class MovePositionPetHandler : ICommandHandler<Guid, MovePositionPetCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MovePositionPetHandler> _logger;
    private readonly IValidator<MovePositionPetCommand> _validator;

    public MovePositionPetHandler(
        [FromKeyedServices(Modules.Pets)]IUnitOfWork unitOfWork,
        IValidator<MovePositionPetCommand> validator,
        ILogger<MovePositionPetHandler> logger,
        IVolunteerRepository volunteerRepository
        )
    {
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
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