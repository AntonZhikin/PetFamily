using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.VolunteerID;
using PetFamily.Domain.Volunteer.VolunteerList;
using PetFamily.Domain.Volunteer.VolunteerValueObject;
using Description = PetFamily.Domain.Volunteer.VolunteerValueObject.Description;
using PhoneNumber = PetFamily.Domain.Volunteer.VolunteerValueObject.PhoneNumber;

namespace PetFamily.Application.Volunteers.CreateVolunteers;

public class CreateVolunteerCommand
{
    private readonly IVolunteerRepository _volunteerRepository;
    
    public CreateVolunteerCommand(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerRequest request, CancellationToken cancellationToken = default)
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

        
        
        var socialNetworks = new List<SocialNetwork>();
        if (request.SocialNetworks != null)
        {
            var details = request.SocialNetworks.Select(sn => SocialNetwork.Create(sn.Path, sn.Name).Value);
            socialNetworks.AddRange(details);
        }
        var voluunterSocialNetworks = new VolunteerSocialNetworks(socialNetworks);
        
        
        
        var assistanceDetails = new List<AssistanceDetail>();
        if (request.AssistanceDetails!= null)
        {
            var details = request.AssistanceDetails.Select(ad => AssistanceDetail.Create(ad.Name, ad.Description).Value);
            assistanceDetails.AddRange(details);
        }
        var volunteerAssistanceDetails = new VolunteerAssistanceDetails(assistanceDetails);

        
        
        var volunteer = new Volunteer(
            volunteerId, 
            descriptionResult.Value, 
            phoneNumberResult.Value, 
            experienceYearsResult.Value, 
            fullNameResult.Value,
            voluunterSocialNetworks,
            volunteerAssistanceDetails
            );

        await _volunteerRepository.Add(volunteer, cancellationToken);

        return volunteer.Id.Value;

        // создание доменной модели

        // сохранение в базу данных
    }
}