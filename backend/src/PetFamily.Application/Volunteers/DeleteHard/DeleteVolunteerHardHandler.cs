using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.Volunteers.DeleteHard;

public class DeleteVolunteerHardHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteVolunteerHardHandler> _logger;
    private readonly IValidator<DeleteVolunteerHardCommand> _validator;


    public DeleteVolunteerHardHandler(
        IVolunteerRepository volunteerRepository, 
        IValidator<DeleteVolunteerHardCommand> validator,
        ILogger<DeleteVolunteerHardHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(DeleteVolunteerHardCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var result = await _volunteerRepository.DeleteHard(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("For volunteer with ID: {id} was hard deleted", volunteerResult.Value.Id);
        
        return result;
    }
}