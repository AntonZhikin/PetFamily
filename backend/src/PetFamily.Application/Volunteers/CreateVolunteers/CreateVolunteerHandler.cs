using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.VolunteerID;
using PetFamily.Domain.Volunteer.VolunteerList;
using PetFamily.Domain.Volunteer.VolunteerValueObject;
using Description = PetFamily.Domain.Volunteer.VolunteerValueObject.Description;
using PhoneNumber = PetFamily.Domain.Volunteer.VolunteerValueObject.PhoneNumber;

namespace PetFamily.Application.Volunteers.CreateVolunteers;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    
    public CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerRequest request, 
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewVolunteerId();

        var descriptionResult = Description.Create(request.Descriptions);
        if (descriptionResult.IsFailure)
            return descriptionResult.Error;

        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumbers);
        if (phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;

        var experienceYearsResult = ExperienceYear.Create(request.ExperienceYears);
        if (experienceYearsResult.IsFailure)
            return experienceYearsResult.Error;

        var fullNameResult = FullName.Create(request.Name, request.Surname, request.SecondName);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error;


        var socialNetwork = request.SocialNetworks
            .Select(s => SocialNetwork.Create(s.Name, s.Path));
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
            descriptionResult.Value, 
            phoneNumberResult.Value, 
            experienceYearsResult.Value, 
            fullNameResult.Value,
            socialNetworks,
            assistanceDetails
            );

        await _volunteerRepository.Add(volunteer, cancellationToken);

        return volunteer.Id.Value;
    }
}