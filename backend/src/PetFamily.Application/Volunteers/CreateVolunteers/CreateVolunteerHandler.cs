using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using FluentValidation;
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
    private readonly IValidator<CreateVolunteerRequest> _validator;

    public CreateVolunteerHandler(
        IVolunteerRepository volunteerRepository,
        IValidator<CreateVolunteerRequest> validator)
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerRequest request, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid == false)
        {
            var error = Error.Validation(
                validationResult.Errors[0].ErrorCode,
                validationResult.Errors[0].ErrorMessage
                );

            return error;
        }
        
        var volunteerId = VolunteerId.NewVolunteerId();

        var description = Description.Create(request.Descriptions).Value;


        var phoneNumber = PhoneNumber.Create(request.PhoneNumbers).Value;

        var experienceYears = ExperienceYear.Create(request.ExperienceYears).Value;
        
        var fullName = FullName.Create(request.Name, request.Surname, request.SecondName).Value;

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
            description, 
            phoneNumber, 
            experienceYears, 
            fullName,
            socialNetworks,
            assistanceDetails
            );

        await _volunteerRepository.Add(volunteer, cancellationToken);

        return volunteer.Id.Value;
    }
}