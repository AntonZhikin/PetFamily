using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;
using PetFamily.VolunteerRequest.Domain.ValueObject;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetRevisionRequiredStatus;

public class SetRevisionRequiredStatusHandler :
    ICommandHandler<Guid, SetRevisionRequiredStatusCommand>
{
    private readonly IValidator<SetRevisionRequiredStatusCommand> _validator;
    private readonly IVolunteerRequestsRepository _volunteerRequestsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SetRevisionRequiredStatusHandler> _logger;

    public SetRevisionRequiredStatusHandler(
        IValidator<SetRevisionRequiredStatusCommand> validator,
        IVolunteerRequestsRepository volunteerRequestsRepository,
        [FromKeyedServices(Modules.VolunteerRequests)] IUnitOfWork unitOfWork,
        ILogger<SetRevisionRequiredStatusHandler> logger)
    {
        _validator = validator;
        _volunteerRequestsRepository = volunteerRequestsRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        SetRevisionRequiredStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(
            command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var existedRequest = await _volunteerRequestsRepository.GetById(
            command.RequestId, cancellationToken);
        if (existedRequest.IsFailure)
            return Errors.General.NotFound(command.RequestId).ToErrorList();
        
        if (existedRequest.Value.AdminId == null)
            return Errors.Review.Failure("Not on review").ToErrorList();
        
        if (existedRequest.Value.RejectionComment != null)
            return Errors.Review.Failure("Rejected").ToErrorList();
        
        var rejectionComment = RejectionComment.Create(command.Comment).Value;
        existedRequest.Value.SetRevisionRequiredStatus(command.AdminId, rejectionComment);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Volunteer request with id {requestId} was rejected", command.RequestId);

        return existedRequest.Value.Id.Value;
    }
}