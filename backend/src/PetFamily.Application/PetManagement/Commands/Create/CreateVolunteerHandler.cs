using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.PetManagement.AggregateRoot;
using PetFamily.Domain.PetManagement.Ids;
using PetFamily.Domain.PetManagement.ValueObjects;
using PetFamily.Domain.Shared.Error;
using Description = PetFamily.Domain.PetManagement.ValueObjects.Description;
using PhoneNumber = PetFamily.Domain.PetManagement.ValueObjects.PhoneNumber;

namespace PetFamily.Application.PetManagement.Commands.Create;

public class CreateVolunteerHandler : ICommandHandler<Guid, CreateVolunteerCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVolunteerCommand> _validator;

    public CreateVolunteerHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<CreateVolunteerHandler> logger,
        IValidator<CreateVolunteerCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        CreateVolunteerCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerId = VolunteerId.NewVolunteerId();

        var description = Description.Create(command.Descriptions).Value;
        
        var phoneNumber = PhoneNumber.Create(command.PhoneNumbers).Value;

        var experienceYears = ExperienceYear.Create(command.ExperienceYears).Value;
        
        var fullName = FullName.Create(command.Name, command.Surname, command.SecondName).Value;

        var socialNetwork = command.SocialNetworkList.SocialNetworks
            .Select(s => SocialNetwork.Create(s.Name, s.Link));
        if (socialNetwork.First().IsFailure)
            return Errors.General.ValueIsInvalid("socialNetworks").ToErrorList();
        
        var socialNetworks = new SocialNetworkList(socialNetwork
            .Select(x => x.Value).ToList());
        if (socialNetworks is null)
            return Errors.General.ValueIsInvalid("socialNetworksList").ToErrorList();
        
        
        var assistanceDetail = command.AssistanceDetailList.AssistanceDetails
            .Select(a => AssistanceDetail.Create(a.Name, a.Description));
        if (assistanceDetail.First().IsFailure)
            return Errors.General.ValueIsInvalid("assistanceDetails").ToErrorList();
        
        var assistanceDetails = new AssistanceDetailList(assistanceDetail
            .Select(x => x.Value).ToList());
        if(assistanceDetails is null)
            return Errors.General.ValueIsInvalid("assistanceDetailsList").ToErrorList();
        
        
        var volunteer = new Volunteer(
            volunteerId, 
            description, 
            phoneNumber, 
            experienceYears, 
            fullName,
            socialNetworks,
            assistanceDetails
            );

        await _volunteerRepository.Add(volunteer);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Created volunteer {name}, with id {id}", fullName.Name, volunteer.Id);

        return volunteer.Id.Value;
    }
}