using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Contracts;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;
using PetFamily.VolunteerRequest.Domain.ValueObject;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetRejectStatus;

public class SetRejectStatusHandler :
    ICommandHandler<Guid, SetRejectStatusCommand>
{
    private readonly IValidator<SetRejectStatusCommand> _validator;
    private readonly IVolunteerRequestsRepository _volunteerRequestsRepository;
    private readonly IAccountsContract _accountContract;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SetRejectStatusHandler> _logger;
    
    public SetRejectStatusHandler(
        IValidator<SetRejectStatusCommand> validator,
        IVolunteerRequestsRepository volunteerRequestsRepository,
        IAccountsContract accountContract,
        [FromKeyedServices(Modules.VolunteerRequests)] IUnitOfWork unitOfWork,
        ILogger<SetRejectStatusHandler> logger)
    {
        _validator = validator;
        _volunteerRequestsRepository = volunteerRequestsRepository;
        _accountContract = accountContract;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        SetRejectStatusCommand command,
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
        existedRequest.Value.SetRejectStatus(
            command.AdminId, rejectionComment);
        
        if(command.IsBanNeed)
            await _accountContract.BanUser(existedRequest.Value.UserId, cancellationToken);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Volunteer request with id {requestId} was rejected", command.RequestId);

        return existedRequest.Value.Id.Value;
    }
}