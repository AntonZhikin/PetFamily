using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.DeleteVolunteer;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.DeleteVolunteerHard;

public class DeleteVolunteerHardHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteVolunteerHardHandler> _logger;

    public DeleteVolunteerHardHandler(
        IVolunteerRepository volunteerRepository, 
        ILogger<DeleteVolunteerHardHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(DeleteVolunteerHardRequest request, CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository.GetById(request.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var result = await _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("For volunteer with ID: {id} was hard deleted", volunteerResult.Value.Id);
        
        return result;
    }
}