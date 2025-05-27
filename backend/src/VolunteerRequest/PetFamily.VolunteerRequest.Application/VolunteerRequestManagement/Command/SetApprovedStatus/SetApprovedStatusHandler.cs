using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;
using PetFamily.VolunteerRequest.Domain.ValueObject;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetApprovedStatus;

public class SetApprovedStatusHandler(
    IValidator<SetApprovedStatusCommand> validator,
    IVolunteerRequestsRepository volunteerRequestsRepository,
    [FromKeyedServices(Modules.VolunteerRequests)]
    IUnitOfWork unitOfWork,
    ILogger<SetApprovedStatusHandler> logger) : ICommandHandler<Guid, SetApprovedStatusCommand>
{
    public async Task<Result<Guid, ErrorList>> Handle(
        SetApprovedStatusCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var existedRequest = await volunteerRequestsRepository.GetById(
            command.RequestId, cancellationToken);
        if (existedRequest.IsFailure)
            return Errors.General.NotFound(command.RequestId).ToErrorList();
        
        if (existedRequest.Value.Status == RequestStatus.Approved)
            return Errors.Review.Failure("Already approved").ToErrorList();
        
        existedRequest.Value.SetApprovedStatus(command.AdminId, command.Comment);
        
        await unitOfWork.SaveChanges(cancellationToken);
        
        logger.LogInformation(
            "Volunteer request with id {requestId} was approved", command.RequestId);

        return existedRequest.Value.Id.Value;
    }
}