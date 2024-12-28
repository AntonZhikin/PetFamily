using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Kernel;

namespace PetFamily.Pets.Application.PetManagement.Commands.UpdatePetMainPhoto;

public class UpdatePetMainPhotoHandler : ICommandHandler<Guid, UpdatePetMainPhotoCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdatePetMainPhotoHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;

    public UpdatePetMainPhotoHandler(
        IUnitOfWork unitOfWork, 
        ILogger<UpdatePetMainPhotoHandler> logger,
        IVolunteerRepository volunteerRepository
        )
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _volunteerRepository = volunteerRepository;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(UpdatePetMainPhotoCommand command, CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var petToUpdate = volunteerResult.Value.Pets.FirstOrDefault(p => p.Id == command.PetId);
        if (petToUpdate == null)
            return Errors.General.NotFound().ToErrorList();
        
        petToUpdate.UpdateMainPhoto(command.PhotoPath);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Pet with Id: {id} was update main photo", petToUpdate.Id);
        
        return volunteerResult.Value.Id.Value;
    }
}