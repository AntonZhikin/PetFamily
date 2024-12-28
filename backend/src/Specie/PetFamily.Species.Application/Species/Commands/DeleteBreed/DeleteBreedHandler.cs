using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;

namespace PetFamily.Species.Application.Species.Commands.DeleteBreed;

public class DeleteBreedHandler : ICommandHandler<Guid, DeleteBreedCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteBreedHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<DeleteBreedCommand> _validator;

    public DeleteBreedHandler(
        IUnitOfWork unitOfWork,
        ILogger<DeleteBreedHandler> logger,
        IReadDbContext readDbContext,
        ISpeciesRepository speciesRepository,
        IValidator<DeleteBreedCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _readDbContext = readDbContext;
        _speciesRepository = speciesRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteBreedCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var petQuery = _readDbContext.Pets.AsQueryable();
        
        var petDto = await petQuery
            .SingleOrDefaultAsync(p => p.SpeciesBreedDto.SpeciesId == command.SpeciesId 
                                       && p.SpeciesBreedDto.BreedId == command.BreedId, cancellationToken);
        if (petDto != null)
            return Errors.General.Found(command.SpeciesId).ToErrorList();
        
        var speciesResult = await _speciesRepository.GetById(command.SpeciesId, cancellationToken);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();
        
        // var breedResult = speciesResult.Value.Breeds
        //     .FirstOrDefault(b => b.Id == command.BreedId);
        // if (breedResult == null)
        //     return Errors.General.NotFound(command.BreedId).ToErrorList();
        
        var result = speciesResult.Value.DeleteBreed(command.BreedId);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Breed with id {breedId} was deleted from specie: {specieId} .",
            command.BreedId, command.SpeciesId);
        
        return result;
    }
}