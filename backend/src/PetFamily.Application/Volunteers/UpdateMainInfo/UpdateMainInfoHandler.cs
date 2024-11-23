using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer.VolunteerValueObject;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;

    public UpdateMainInfoHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateMainInfoHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdateMainInfoRequest request, 
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _volunteerRepository.GetById(request.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var description = Description.Create(request.Dto.Descriptions).Value;
        
        var phoneNumber = PhoneNumber.Create(request.Dto.PhoneNumbers).Value;

        var experienceYears = ExperienceYear.Create(request.Dto.ExperienceYears).Value;
        
        var fullName = FullName.Create(request.Dto.Name, request.Dto.Surname, request.Dto.SecondName).Value;
        
        volunteerResult.Value.UpdateMainInfo(description, phoneNumber, experienceYears, fullName);
        
        var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation( "For volunteer with ID: {id} was updated informations", volunteerResult.Value.Id);
        
        return result;
    }
}