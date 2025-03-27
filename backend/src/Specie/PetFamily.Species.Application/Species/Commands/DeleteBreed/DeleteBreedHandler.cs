using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;
using PetFamily.Pets.Contracts;
using PetFamily.Pets.Contracts.Request;

namespace PetFamily.Species.Application.Species.Commands.DeleteBreed;

public class DeleteBreedHandler : ICommandHandler<Guid, DeleteBreedCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteBreedHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<DeleteBreedCommand> _validator;
    private readonly IPetsContracts _petsContracts;

    public DeleteBreedHandler(
        [FromKeyedServices(Modules.Specie)]IUnitOfWork unitOfWork,
        ILogger<DeleteBreedHandler> logger,
        IReadDbContext readDbContext,
        ISpeciesRepository speciesRepository,
        IValidator<DeleteBreedCommand> validator,
        IPetsContracts petsContracts)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _readDbContext = readDbContext;
        _speciesRepository = speciesRepository;
        _validator = validator;
        _petsContracts = petsContracts;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteBreedCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var petQuery = _readDbContext.Pets.AsQueryable();
        
        var petDto = await _petsContracts
            .AnyPetWithSpeciesId(new AnyPetWithSpeciesIdRequest(command.SpeciesId), cancellationToken);
        if (petDto != null)
            return Errors.General.Found(command.SpeciesId).ToErrorList();
        
        var petDtoBreed = await _petsContracts
            .AnyPetWithBreedId(new AnyPetWithBreedIdRequest(command.BreedId), cancellationToken);
        if (petDtoBreed != null)
            return Errors.General.Found(command.SpeciesId).ToErrorList();
        
        var speciesResult = await _speciesRepository.GetById(command.SpeciesId, cancellationToken);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();
        
        speciesResult.Value.Delete();
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Breed with id {breedId} was deleted from specie: {specieId} .",
            command.BreedId, command.SpeciesId);
        
        return Result.Success(command.BreedId).Value;
    }
}