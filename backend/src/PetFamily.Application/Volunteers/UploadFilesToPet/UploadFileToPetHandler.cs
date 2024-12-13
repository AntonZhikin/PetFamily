using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Files;
using PetFamily.Application.Messaging;
using PetFamily.Domain.PetManagement.Ids;
using PetFamily.Domain.PetManagement.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;
using FileInfo = PetFamily.Application.Files.FileInfo;
using IFileProvider = PetFamily.Application.Files.IFileProvider;

namespace PetFamily.Application.Volunteers.UploadFilesToPet;

public class UploadFileToPetHandler
{
    private const string BUCKET_NAME = "photo";

    private readonly IFileProvider _fileProvider;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<UploadFileToPetCommand> _validator;
    private readonly ILogger<UploadFileToPetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;


    public UploadFileToPetHandler(
        IFileProvider fileProvider,
        IValidator<UploadFileToPetCommand> validator,
        IVolunteerRepository volunteerRepository,
        ILogger<UploadFileToPetHandler> logger,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue,
        IUnitOfWork unitOfWork)
    {
        _fileProvider = fileProvider;
        _volunteerRepository = volunteerRepository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _messageQueue = messageQueue;
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
            
            var fileData = new FileData(file.Content, new FileInfo(filePath.Value, BUCKET_NAME));
            
            filesData.Add(fileData);
        }
        
        var filePathsResult = await _fileProvider.UploadFiles(filesData, cancellationToken);
        if (filePathsResult.IsFailure)
        {
            await _messageQueue.WriteAsync(filesData.Select(f => f.Info), cancellationToken);
            
            return filePathsResult.Error.ToErrorList();
        }
        
        var petFiles = filePathsResult.Value
            .Select(f => new PetPhoto(f, true))
            .ToList();
        
        petResult.Value.UpdatePhotos(petFiles);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Succes uploaded files to pet - {id}", petResult.Value.Id.Value);

        return petResult.Value.Id.Value; 
    }
}