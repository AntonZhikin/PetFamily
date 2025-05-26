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

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.SetReopenStatus;

public class SetReopenStatusHandler :
    ICommandHandler<Guid, SetReopenStatusCommand>
{
    private readonly IValidator<SetReopenStatusCommand> _validator;
    private readonly IAccountsContract _accountContracts;
    private readonly IVolunteerRequestsRepository _volunteerRequestsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SetReopenStatusHandler> _logger;

    public SetReopenStatusHandler(
        IValidator<SetReopenStatusCommand> validator,
        IAccountsContract accountContracts,
        IVolunteerRequestsRepository volunteerRequestsRepository,
        [FromKeyedServices(Modules.VolunteerRequests)] IUnitOfWork unitOfWork,
        ILogger<SetReopenStatusHandler> logger)
    {
        _validator = validator;
        _accountContracts = accountContracts;
        _volunteerRequestsRepository = volunteerRequestsRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        SetReopenStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(
            command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var isUserBanned = await _accountContracts.IsUserBannedForVolunteerRequests(command.UserId, cancellationToken);
        if (isUserBanned)
            return Errors.Review.Failure("user is banned").ToErrorList();
        
        var existedRequest = await _volunteerRequestsRepository.GetById(
            command.RequestId, cancellationToken);
        if (existedRequest.IsFailure)
            return Errors.General.NotFound(command.RequestId).ToErrorList();
        
        if (existedRequest.Value.Status == RequestStatus.Submitted)
            return Errors.Review.Failure("already submitted").ToErrorList();
        
        existedRequest.Value.Refresh(command.UserId, command.Comment);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation(
            "Volunteer request with id {requestId} was reopened with submitted status",
            command.RequestId);

        return existedRequest.Value.Id.Value;
    }
}