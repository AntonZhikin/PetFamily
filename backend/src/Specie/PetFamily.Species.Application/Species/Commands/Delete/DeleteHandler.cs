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

namespace PetFamily.Species.Application.Species.Commands.Delete;

public class DeleteHandler : ICommandHandler<Guid, DeleteCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<DeleteCommand> _validator;
    private readonly IPetsContracts _petsContracts;

    public DeleteHandler(
        [FromKeyedServices(Modules.Specie)]IUnitOfWork unitOfWork,
        ILogger<DeleteHandler> logger,
        IReadDbContext readDbContext,
        ISpeciesRepository speciesRepository,
        IValidator<DeleteCommand> validator,
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
        DeleteCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var petQuery = _readDbContext.Pets.AsQueryable();
        
        var petDto = await _petsContracts
            .AnyPetWithSpeciesId(new AnyPetWithSpeciesIdRequest(command.SpeciesId), cancellationToken);
        if (petDto != null)
            return Errors.General.Found(command.SpeciesId).ToErrorList();
        
        var speciesResult = await _speciesRepository.GetById(command.SpeciesId, cancellationToken);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var result= await _speciesRepository.Delete(speciesResult.Value);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("For species with ID: {id} was deleted", speciesResult.Value.Id);
        
        return result;
    }
}