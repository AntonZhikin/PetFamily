using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.PetManagement.ValueObjects;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.PetManagement.Commands.UpdateSocialNetworks;

public class UpdateSocialNetworkHandler : ICommandHandler<Guid ,UpdateSocialNetworkCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateSocialNetworkHandler> _logger;
    private readonly IValidator<UpdateSocialNetworkCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateSocialNetworkHandler(
        IVolunteerRepository volunteerRepository,
        IValidator<UpdateSocialNetworkCommand> validator,
        ILogger<UpdateSocialNetworkHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateSocialNetworkCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
        {
            return volunteerResult.Error.ToErrorList();
        }

        var socialNetworks = command.SocialNetworkList.SocialNetworks
            .Select(dto => new SocialNetwork(dto.Name, dto.Link))
            .ToList();
        
        var volunteerSocialNetworks = new SocialNetworkList(socialNetworks);

        volunteerResult.Value.UpdateSocialNetworks(volunteerSocialNetworks);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Main info of volunteer updated with id: {VolunteerId}.", command.VolunteerId);

        return volunteerResult.Value.Id.Value;
    }
}