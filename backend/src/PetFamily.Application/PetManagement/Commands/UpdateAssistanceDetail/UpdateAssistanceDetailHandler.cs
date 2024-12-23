using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.PetManagement.ValueObjects;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.PetManagement.Commands.UpdateAssistanceDetail;

public class UpdateAssistanceDetailHandler : ICommandHandler<Guid, UpdateAssistanceDetailCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateAssistanceDetailHandler> _logger;
    private readonly IValidator<UpdateAssistanceDetailCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateAssistanceDetailHandler(
        IVolunteerRepository volunteerRepository,
        IValidator<UpdateAssistanceDetailCommand> validator,
        ILogger<UpdateAssistanceDetailHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(UpdateAssistanceDetailCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
        {
            return volunteerResult.Error.ToErrorList();
        }
        
        var assistanceDetail = command.AssistanceDetailList.AssistanceDetails
            .Select(dto => new AssistanceDetail(dto.Name, dto.Description))
            .ToList();
        
        var volunteerAssistanceDetails = new AssistanceDetailList(assistanceDetail);
        
        volunteerResult.Value.UpdateAssistanceDetail(volunteerAssistanceDetails);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("AssistanceDetail of volunteer updated with id: {VolunteerId}.", command.VolunteerId);

        return volunteerResult.Value.Id.Value;
    }
}