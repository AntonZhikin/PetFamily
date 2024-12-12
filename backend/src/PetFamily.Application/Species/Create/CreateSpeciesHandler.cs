using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;
using PetFamily.Domain.SpeciesManagement.AggregateRoot;
using PetFamily.Domain.SpeciesManagement.Ids;

namespace PetFamily.Application.Species.Create;

public class CreateSpeciesHandler
{
    private readonly IValidator<CreateSpeciesCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<CreateSpeciesHandler> _logger;


    public CreateSpeciesHandler(
        IValidator<CreateSpeciesCommand> validator,
        IUnitOfWork unitOfWork,
        ISpeciesRepository speciesRepository,
        ILogger<CreateSpeciesHandler> logger
        )
    {
        _validator = validator;
        _unitOfWork = unitOfWork;
        _speciesRepository = speciesRepository;
        _logger = logger;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        CreateSpeciesCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var speciesId = SpeciesId.NewSpeciesId();
        
        var name = Name.Create(command.Name).Value;
        
        var species = new Domain.SpeciesManagement.AggregateRoot.Species(speciesId, name);
        
        await _speciesRepository.Add(species);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Created species {name}, with id {id}", command.Name, species.Id);
        
        return species.Id.Value;
    }
}