using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Pet.PetID;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer.VolunteerID;
using IFileProvider = PetFamily.Application.FileProvider.IFileProvider;

namespace PetFamily.Application.Volunteers.UploadFilesToPet;

public class UploadFileToPetHandler
{
    private const string BUCKET_NAME = "photo";

    private readonly IFileProvider _fileProvider;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<UploadFileToPetCommand> _validator;
    private readonly ILogger<UploadFileToPetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;


    public UploadFileToPetHandler(
        IFileProvider fileProvider,
        IValidator<UploadFileToPetCommand> validator,
        IVolunteerRepository volunteerRepository,
        ILogger<UploadFileToPetHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _fileProvider = fileProvider;
        _volunteerRepository = volunteerRepository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UploadFileToPetCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var volunteerResult = await _volunteerRepository
            .GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        //var commandPetId = command.PetId;
        var petId = PetId.Create(command.PetId);
        
        var petResult = volunteerResult.Value.GetPetById(petId);
        if(petResult.IsFailure)
            return petResult.Error.ToErrorList();

        List<FileData> filesData = [];
        foreach (var file in command.Files)
        {
            var extension = Path.GetExtension(file.FileName);
            
            var filePath = PhotoPath.Create(Guid.NewGuid(), extension);
            if(filePath.IsFailure)
                return filePath.Error.ToErrorList();
            
            var fileData = new FileData(file.Content, filePath.Value, BUCKET_NAME);
            
            filesData.Add(fileData);
        }
        
        var filePathsResult = await _fileProvider.UploadFiles(filesData, cancellationToken);
        if (filePathsResult.IsFailure)
            return filePathsResult.Error.ToErrorList();
        
        var petFiles = filePathsResult.Value
            .Select(f => new PetPhoto(f, true))
            .ToList();
        
        petResult.Value.UpdatePhotos(petFiles);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Succes uploaded files to pet - {id}", petResult.Value.Id.Value);

        return petResult.Value.Id.Value; 
    }
}