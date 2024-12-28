using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs.ValueObject;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;
using PetFamily.Pets.Domain.ValueObjects;

namespace PetFamily.Pets.Application.PetManagement.Commands.UpdatePetStatus;

public class UpdatePetStatusHandler : ICommandHandler<Guid, UpdatePetStatusCommand>
{
    private readonly ILogger<UpdatePetStatusHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<UpdatePetStatusCommand> _validator;

    public UpdatePetStatusHandler(
        ILogger<UpdatePetStatusHandler> logger,
        IUnitOfWork unitOfWork,
        IVolunteerRepository volunteerRepository,
        IValidator<UpdatePetStatusCommand> validator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _volunteerRepository = volunteerRepository;
        _validator = validator;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(UpdatePetStatusCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var newStatus = Enum.Parse<HelpStatus>(command.NewStatus);
        
        var pet = volunteerResult.Value.Pets.FirstOrDefault(i => i.Id == command.PetId);
        if (pet == null)
            return Errors.General.NotFound().ToErrorList();
        
        pet.UpdateStatus(newStatus);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("For pet with Id: {id} was updated status", pet.Id);

        return volunteerResult.Value.Id.Value;
    }
}