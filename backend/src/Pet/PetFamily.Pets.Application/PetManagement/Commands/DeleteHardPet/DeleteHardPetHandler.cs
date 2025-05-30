using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Core.File;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject;
using PetFamily.Kernel.ValueObject.Ids;
using FileInfo = PetFamily.Core.File.FileInfo;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteHardPet;

public class DeleteHardPetHandler : ICommandHandler<Guid, DeleteHardPetCommand>
{
    private const string BUCKET_NAME = "photos";
    
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteHardPetHandler> _logger;
    private readonly IValidator<DeleteHardPetCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileProvider _fileProvider;
    
    public DeleteHardPetHandler(
        IVolunteerRepository volunteerRepository, 
        IValidator<DeleteHardPetCommand> validator,
        ILogger<DeleteHardPetHandler> logger,
        [FromKeyedServices(Modules.Pets)]IUnitOfWork unitOfWork,
        IFileProvider fileProvider)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _fileProvider = fileProvider;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteHardPetCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var petId = PetId.Create(command.PetId);
        
        var petResult = volunteerResult.Value.GetPetById(petId);
        if (petResult.IsFailure)
            return Errors.General.NotFound(command.PetId).ToErrorList();
        
        var photosFileInfo = petResult.Value.Photos
            .Select(p => new FileInfo(PhotoPath
                .Create(p.PathToStorage.ToString()).Value, BUCKET_NAME));
        
        foreach (var photo in photosFileInfo)
        {
            await _fileProvider.RemoveFile(photo, cancellationToken);
        }
        
        volunteerResult.Value.HardDeletePet(petResult.Value);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Pet with Id: {id} was deleted hard", command.PetId);
        
        return command.PetId;
    }
}