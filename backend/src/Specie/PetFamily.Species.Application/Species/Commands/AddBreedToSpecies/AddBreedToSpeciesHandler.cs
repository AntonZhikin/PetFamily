using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.Species.Domain.SpeciesManagement.Entity;

namespace PetFamily.Species.Application.Species.Commands.AddBreedToSpecies;

public class AddBreedToSpeciesHandler : ICommandHandler<Guid, AddBreedToSpeciesCommand>
{
    private readonly IValidator<AddBreedToSpeciesCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<AddBreedToSpeciesHandler> _logger;


    public AddBreedToSpeciesHandler(
        IValidator<AddBreedToSpeciesCommand> validator,
        [FromKeyedServices(Modules.Specie)]IUnitOfWork unitOfWork,
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