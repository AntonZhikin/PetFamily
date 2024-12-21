using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.PetManagement.Commands.DeleteSoft;
using PetFamily.Domain.PetManagement.Ids;
using PetFamily.Domain.PetManagement.ValueObjects;
using PetFamily.Domain.Shared.Error;
using FileInfo = PetFamily.Application.Files.FileInfo;

namespace PetFamily.Application.PetManagement.Commands.DeleteHardPet;

public class DeleteHardPetHandler : ICommandHandler<Guid, DeleteHardPetCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteHardPetHandler> _logger;
    private readonly IValidator<DeleteHardPetCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteHardPetHandler(
        IVolunteerRepository volunteerRepository, 
        IValidator<DeleteHardPetCommand> validator,
        ILogger<DeleteHardPetHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(DeleteHardPetCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var pet = volunteerResult.Value.Pets.FirstOrDefault(i => i.Id == command.PetId);
        if (pet == null)
            return Errors.General.NotFound().ToErrorList();
        
        volunteerResult.Value.DeletePet(pet);
        
        var petId = PetId.Create(command.PetId);
        
        var petPhotos = volunteerResult.Value.DeletePetPhotos(petId);
        if (petPhotos.IsFailure)
            return petPhotos.Error.ToErrorList();
        
        var sortedPetList = volunteerResult.Value.Pets.OrderBy(i => i.Position.Value).ToList();
        
        volunteerResult.Value.UpdatePetPosition(sortedPetList);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Pet with Id: {id} was deleted", command.PetId);
        
        return command.PetId;
    }
}