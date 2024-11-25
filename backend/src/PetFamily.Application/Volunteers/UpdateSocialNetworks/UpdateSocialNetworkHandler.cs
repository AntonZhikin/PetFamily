using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer.VolunteerList;
using PetFamily.Domain.Volunteer.VolunteerValueObject;

namespace PetFamily.Application.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworkHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateSocialNetworkHandler> _logger;

    public UpdateSocialNetworkHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateSocialNetworkHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(UpdateSocialNetworkRequest request, CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository.GetById(request.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
        {
            return volunteerResult.Error;
        }

        var socialNetworks = request.SocialNetworkDto.SocialNetworks
            .Select(dto => new SocialNetwork(dto.Name, dto.Link))
            .ToList();
        
        var volunteerSocialNetworks = new VolunteerSocialNetworks(socialNetworks);

        volunteerResult.Value.UpdateSocialNetworks(volunteerSocialNetworks);

        var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

        _logger.LogInformation( "For volunteer with ID: {id} was updated social networks", volunteerResult.Value.Id);
        
        return result;
    }
}