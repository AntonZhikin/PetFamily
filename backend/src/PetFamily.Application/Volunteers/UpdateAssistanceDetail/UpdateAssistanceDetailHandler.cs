using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer.VolunteerList;
using PetFamily.Domain.Volunteer.VolunteerValueObject;

namespace PetFamily.Application.Volunteers.UpdateAssistanceDetail;

public class UpdateAssistanceDetailHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateAssistanceDetailHandler> _logger;

    public UpdateAssistanceDetailHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateAssistanceDetailHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }
    
    public async Task<Result<Guid, Error>> Handle(UpdateAssistanceDetailRequest request, CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository.GetById(request.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
        {
            return volunteerResult.Error;
        }
        
        var assistanceDetail = request.AssistanceDetailDto.AssistanceDetails
            .Select(dto => new AssistanceDetail(dto.Name, dto.Description))
            .ToList();
        
        var volunteerAssistanceDetails = new VolunteerAssistanceDetails(assistanceDetail);
        
        volunteerResult.Value.UpdateAssistanceDetail(volunteerAssistanceDetails);
        
        var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("For volunteer with ID: {id} was updated assistancedetail", volunteerResult.Value.Id);
        
        return result;
    }
}