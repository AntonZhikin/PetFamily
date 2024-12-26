using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.Species.Commands.Delete;

public class DeleteHandler : ICommandHandler<Guid, DeleteCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<DeleteCommand> _validator;

    public DeleteHandler(
        IUnitOfWork unitOfWork,
        ILogger<DeleteHandler> logger,
        IReadDbContext readDbContext,
        ISpeciesRepository speciesRepository,
        IValidator<DeleteCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _readDbContext = readDbContext;
        _speciesRepository = speciesRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var petQuery = _readDbContext.Pets.AsQueryable();
        
        var petDto = await petQuery
            .SingleOrDefaultAsync(p => p.SpeciesBreedDto.SpeciesId == command.SpeciesId, cancellationToken);
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