using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer.VolunteerValueObject;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;
    private readonly IValidator<UpdateMainInfoCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMainInfoHandler(
        IVolunteerRepository volunteerRepository,
        IValidator<UpdateMainInfoCommand> validator,
        ILogger<UpdateMainInfoHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateMainInfoCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var description = Description.Create(command.Descriptions).Value;
        
        var phoneNumber = PhoneNumber.Create(command.PhoneNumbers).Value;

        var experienceYears = ExperienceYear.Create(command.ExperienceYears).Value;
        
        var fullName = FullName.Create(command.Name, command.Surname, command.SecondName).Value;
        
        volunteerResult.Value.UpdateMainInfo(description, phoneNumber, experienceYears, fullName);
        
        _volunteerRepository.Save(volunteerResult.Value);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Main info of volunteer updated with id: {VolunteerId}.", command.VolunteerId);

        return volunteerResult.Value.Id.Value;
    }
}