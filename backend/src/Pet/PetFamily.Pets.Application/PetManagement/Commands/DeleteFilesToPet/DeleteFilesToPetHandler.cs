using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Core.File;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject.Ids;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteFilesToPet;

public class DeleteFilesToPetHandler : ICommandHandler<Guid, DeleteFilesToPetCommand>
{
    private readonly IValidator<DeleteFilesToPetCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteFilesToPetHandler> _logger;
    private readonly IFileProvider _fileProvider;

    public DeleteFilesToPetHandler(
        IValidator<DeleteFilesToPetCommand> validator,
        [FromKeyedServices(Modules.Pets)]IUnitOfWork unitOfWork,
        IVolunteerRepository volunteerRepository,
        ILogger<DeleteFilesToPetHandler> logger,
        IFileProvider fileProvider)
    {
        _validator = validator;
        _unitOfWork = unitOfWork;
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _fileProvider = fileProvider;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteFilesToPetCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            validationResult.ToErrorList();
        
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petId = PetId.Create(command.PetId);
        
        var petResult = volunteerResult.Value.GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        var deleteResult = volunteerResult.Value.DeletePetPhotos(petId);
        if (deleteResult.IsFailure)
            return deleteResult.Error.ToErrorList();
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Success deleted all pet photos of pet with id {petId}", petId);
        
        return petResult.Value.Id.Value;
    }
}