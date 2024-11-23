using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.VolunteerID;
using PetFamily.Domain.Volunteer.VolunteerList;
using PetFamily.Domain.Volunteer.VolunteerValueObject;
using Description = PetFamily.Domain.Volunteer.VolunteerValueObject.Description;
using PhoneNumber = PetFamily.Domain.Volunteer.VolunteerValueObject.PhoneNumber;

namespace PetFamily.Application.Volunteers.Create;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;

    public CreateVolunteerHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerRequest request, 
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewVolunteerId();

        var description = Description.Create(request.Descriptions).Value;
        
        var phoneNumber = PhoneNumber.Create(request.PhoneNumbers).Value;

        var experienceYears = ExperienceYear.Create(request.ExperienceYears).Value;
        
        var fullName = FullName.Create(request.Name, request.Surname, request.SecondName).Value;

        var socialNetwork = request.SocialNetworks
            .Select(s => SocialNetwork.Create(s.Name, s.Link));
        if (socialNetwork.First().IsFailure)
            return Errors.General.ValueIsInvalid("socialNetworks");
        
        var socialNetworks = new VolunteerSocialNetworks(socialNetwork
            .Select(x => x.Value).ToList());
        if (socialNetworks is null)
            return Errors.General.ValueIsInvalid("socialNetworksList");
        
        
        var assistanceDetail = request.AssistanceDetails
            .Select(a => AssistanceDetail.Create(a.Name, a.Description));
        if (assistanceDetail.First().IsFailure)
            return Errors.General.ValueIsInvalid("assistanceDetails");
        
        var assistanceDetails = new VolunteerAssistanceDetails(assistanceDetail
            .Select(x => x.Value).ToList());
        if(assistanceDetails is null)
            return Errors.General.ValueIsInvalid("assistanceDetailsList");
        
        
        var volunteer = new Volunteer(
            volunteerId, 
            description, 
            phoneNumber, 
            experienceYears, 
            fullName,
            socialNetworks,
            assistanceDetails
            );

        await _volunteerRepository.Add(volunteer, cancellationToken);
        
        _logger.LogInformation("Created volunteer {name}, with id {id}", fullName.Name, volunteer.Id);

        return volunteer.Id.Value;
    }
}