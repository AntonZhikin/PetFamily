using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteHard;

public class DeleteVolunteerHardHandler : ICommandHandler<Guid, DeleteVolunteerHardCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteVolunteerHardHandler> _logger;
    private readonly IValidator<DeleteVolunteerHardCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;


    public DeleteVolunteerHardHandler(
        IVolunteerRepository volunteerRepository, 
        IValidator<DeleteVolunteerHardCommand> validator,
        ILogger<DeleteVolunteerHardHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
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
        
        var result = await _volunteerRepository.DeleteHard(volunteerResult.Value);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("For volunteer with ID: {id} was hard deleted", volunteerResult.Value.Id);
        
        return result;
    }
}