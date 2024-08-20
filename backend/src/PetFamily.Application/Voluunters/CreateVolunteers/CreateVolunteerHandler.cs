using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using Description = PetFamily.Domain.Volunteer.Description;
using PhoneNumber = PetFamily.Domain.Volunteer.PhoneNumber;

namespace PetFamily.Application.Voluunters.CreateVoluunters;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    
    public CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerRequest request, CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewVolunteerId();

        var descriptionResult = Description.Create(request.descriptions);
        if (descriptionResult.IsFailure)
            return descriptionResult.Error;

        var phoneNumberResult = PhoneNumber.Create(request.phoneNumbers);
        if (phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;

        var experienceYearsResult = ExperienceYear.Create(request.experienceYears);
        if (experienceYearsResult.IsFailure)
            return experienceYearsResult.Error;

        var fullNameResult = FullName.Create(request.name, request.surname, request.secondname);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error;

        var volunteer = new Volunteer(
            volunteerId, 
            descriptionResult.Value, 
            phoneNumberResult.Value, 
            experienceYearsResult.Value, 
            fullNameResult.Value
            );

        await _volunteerRepository.Add(volunteer, cancellationToken);

        return volunteer.Id.Value;

        // создание доменной модели

        // сохранение в базу данных
    }
}