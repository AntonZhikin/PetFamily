using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.TakeInReview;

public class TakeInReviewHandler(
    IValidator<TakeInReviewCommand> validator,
    IVolunteerRequestsRepository volunteerRequestsRepository,
    [FromKeyedServices(Modules.VolunteerRequests)]
    IUnitOfWork unitOfWork,
    ILogger<TakeInReviewHandler> logger) : ICommandHandler<Guid, TakeInReviewCommand>
{
    public async Task<Result<Guid, ErrorList>> Handle(
        TakeInReviewCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(
            command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var existedRequest = await volunteerRequestsRepository.GetById(
            command.RequestId, cancellationToken);
        if (existedRequest.IsFailure)
            return Errors.General.NotFound(command.RequestId).ToErrorList();
        
        if (existedRequest.Value.AdminId != null)
            return Errors.Review.Failure("already on review").ToErrorList();
        
        existedRequest.Value.TakeInReview(command.AdminId);
        
        await unitOfWork.SaveChanges(cancellationToken);
        
        logger.LogInformation("Volunteer request {requestId} was taken on review", command.RequestId);

        return existedRequest.Value.Id.Value;
    }
}