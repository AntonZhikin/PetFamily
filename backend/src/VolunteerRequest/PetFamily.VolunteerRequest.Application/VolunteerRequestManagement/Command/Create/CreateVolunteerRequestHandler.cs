using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Contracts;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.VolunteerRequest.Domain.ValueObject;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Command.Create;

public class CreateVolunteerRequestHandler :
    ICommandHandler<Guid, CreateVolunteerRequestCommand>
{
    private readonly IValidator<CreateVolunteerRequestCommand> _validator;
    private readonly IVolunteerRequestsRepository _volunteerRequestsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountsContract _accountsContract;
    private readonly ILogger<CreateVolunteerRequestHandler> _logger;

    public CreateVolunteerRequestHandler(
        IValidator<CreateVolunteerRequestCommand> validator,
        IVolunteerRequestsRepository volunteerRequestsRepository,
        [FromKeyedServices(Modules.VolunteerRequests)] IUnitOfWork unitOfWork,
        ILogger<CreateVolunteerRequestHandler> logger, IAccountsContract accountsContract)
    {
        _validator = validator;
        _volunteerRequestsRepository = volunteerRequestsRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _accountsContract = accountsContract;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateVolunteerRequestCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(
            command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var isUserBanned = await _accountsContract.IsUserBannedForVolunteerRequests(command.UserId, cancellationToken);
        if (isUserBanned)
            return Errors.Review.Failure("user is banned").ToErrorList();
        
        var requestId = VolunteerRequestId.Create(Guid.NewGuid());
        
        var fullName = FullName.Create(
            command.FullName.Name,
            command.FullName.Surname,
            command.FullName.SecondName).Value;
        
        
        var volunteerInfo = VolunteerInfo.Create(command.VolunteerInfo.Age).Value;
        
        var newVolunteerRequest = Domain.VolunteerRequest.Create(
            requestId,
            command.UserId, fullName, volunteerInfo).Value;

        await _volunteerRequestsRepository.Add(newVolunteerRequest, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation(
            "Volunteer request was created with id {requestId}", requestId);

        return newVolunteerRequest.Id.Value;
    }
}