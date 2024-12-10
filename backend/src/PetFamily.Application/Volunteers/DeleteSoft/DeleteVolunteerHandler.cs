using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.DeleteSoft;

public class DeleteVolunteerHandler
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
        
        _volunteerRepository.Save(volunteerResult.Value);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("For volunteer with ID: {id} was deleted", volunteerResult.Value.Id);
        
        return volunteerResult.Value.Id.Value;
    }
}