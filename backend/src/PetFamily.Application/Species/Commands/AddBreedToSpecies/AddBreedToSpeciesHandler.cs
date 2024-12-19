using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Species.Create;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;
using PetFamily.Domain.SpeciesManagement.Entity;
using PetFamily.Domain.SpeciesManagement.Ids;

namespace PetFamily.Application.Species.AddBreedToSpecies;

public class AddBreedToSpeciesHandler : ICommandHandler<Guid, AddBreedToSpeciesCommand>
{
    private readonly IValidator<AddBreedToSpeciesCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<AddBreedToSpeciesHandler> _logger;


    public AddBreedToSpeciesHandler(
        IValidator<AddBreedToSpeciesCommand> validator,
        IUnitOfWork unitOfWork,
        ISpeciesRepository speciesRepository,
        ILogger<AddBreedToSpeciesHandler> logger
    )
    {
        _validator = validator;
        _unitOfWork = unitOfWork;
        _speciesRepository = speciesRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddBreedToSpeciesCommand command, 
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var speciesResult = await _speciesRepository
            .GetById(command.SpeciesId, cancellationToken);
        if(speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var breedId = BreedId.New();
        var name = Name.Create(command.Name).Value;
        
        var breed = new Breed(
            breedId, 
            name);
        
        speciesResult.Value.AddBreed(breed);
        
        _speciesRepository.Save(speciesResult.Value);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Breed added with id {breedId}.", breedId.Value);

        return breed.Id.Value;
    }
}