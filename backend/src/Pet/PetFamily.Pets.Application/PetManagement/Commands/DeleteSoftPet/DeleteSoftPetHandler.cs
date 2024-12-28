using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteSoftPet;

public class DeleteSoftPetHandler : ICommandHandler<Guid, DeleteSoftPetCommand>
{
    private readonly ILogger<DeleteSoftPetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<DeleteSoftPetCommand> _validator;

    public DeleteSoftPetHandler(
        ILogger<DeleteSoftPetHandler> logger,
        IUnitOfWork unitOfWork,
        IVolunteerRepository volunteerRepository,
        IValidator<DeleteSoftPetCommand> validator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _volunteerRepository = volunteerRepository;
        _validator = validator;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(DeleteSoftPetCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var pet = volunteerResult.Value.Pets.FirstOrDefault(i => i.Id == command.PetId);
        if (pet == null)
            return Errors.General.NotFound().ToErrorList();
        
        pet.Delete();
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("For pet with Id: {id} was soft deleted", pet.Id);
        
        return volunteerResult.Value.Id.Value;
    }
}