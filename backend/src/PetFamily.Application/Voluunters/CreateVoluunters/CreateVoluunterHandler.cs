using PetFamily.Domain.Volunteer;
using Description = PetFamily.Domain.Volunteer.Description;
using PhoneNumber = PetFamily.Domain.Volunteer.PhoneNumber;

namespace PetFamily.Application.Voluunters.CreateVoluunters;

public class CreateVoluunterHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    
    public CreateVoluunterHandler(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }
    
    public async Task<Guid> HandleAsync(
        CreateVoluunterRequest request, CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewVolunteerId();

        var descriptionsResult = Description.Create(request.descriptions);

        var phoneNumberResult = PhoneNumber.Create(request.phoneNumbers);

        var experienceYearsResult = ExperienceYear.Create(request.experienceYears);

        var fullNameResult = FullName.Create(request.name, request.surname, request.secondname);

        var volunteer = new Volunteer(
            volunteerId, 
            descriptionsResult, 
            phoneNumberResult, 
            experienceYearsResult, 
            fullNameResult
            );

        await _volunteerRepository.Add(volunteer, cancellationToken);

        return volunteer.Id.Value;

        // создание доменной модели

        // сохранение в базу данных
    }
}