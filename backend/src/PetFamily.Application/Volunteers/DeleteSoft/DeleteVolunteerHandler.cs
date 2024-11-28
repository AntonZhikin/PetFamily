using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.DeleteVolunteer;

public class DeleteVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteVolunteerHandler> _logger;

    public DeleteVolunteerHandler(
        IVolunteerRepository volunteerRepository, 
        ILogger<DeleteVolunteerHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(DeleteVolunteerRequest request, CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository.GetById(request.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        volunteerResult.Value.Delete();
        
        var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("For volunteer with ID: {id} was deleted", volunteerResult.Value.Id);
        
        return result;
    }
}