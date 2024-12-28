using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteSoft;

public class DeleteVolunteerHandler : ICommandHandler<Guid, DeleteVolunteerCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IValidator<DeleteVolunteerCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVolunteerHandler(
        IVolunteerRepository volunteerRepository, 
        IValidator<DeleteVolunteerCommand> validator,
        ILogger<DeleteVolunteerHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteVolunteerCommand command, CancellationToken cancellationToken)
    { 
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        volunteerResult.Value.SoftDelete();
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("For volunteer with ID: {id} was deleted", volunteerResult.Value.Id);
        
        return volunteerResult.Value.Id.Value;
    }
}