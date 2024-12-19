using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Species.Request;
using PetFamily.API.Controllers.Volunteers.Request;
using PetFamily.API.Extensions;
using PetFamily.Application.Species.AddBreedToSpecies;
using PetFamily.Application.Species.Create;
using PetFamily.Application.Species.Delete;
using PetFamily.Application.Species.DeleteBreed;
using PetFamily.Application.Species.Queries;
using PetFamily.Application.Species.Queries.GetBreedByIdSpecies;
using PetFamily.Domain.Shared;

namespace PetFamily.API.Controllers.Species;

public class SpeciesController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody] CreateSpeciesRequest request,
        [FromServices] CreateSpeciesHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }

    [HttpPost("{id:guid}breed")]
    public async Task<ActionResult> AddBreed(
        [FromRoute] Guid id,
        [FromBody] AddBreedToSpeciesRequest request,
        [FromServices] AddBreedToSpeciesHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);
        if(result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCommand(id);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpDelete("{speciesId:guid}/breed/{breedId:guid}")]
    public async Task<ActionResult> DeleteBreed(
        [FromRoute] Guid speciesId,
        [FromRoute] Guid breedId,
        [FromServices] DeleteBreedHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteBreedCommand(speciesId, breedId);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetSpeciesWithPaginationRequest request,
        [FromServices] GetSpeciesWithPaginationHandler handler,
        CancellationToken cancellationToken = default)
    {
        var queryResult = request.ToQuery();
        
        var response = await handler.Handle(queryResult, cancellationToken);
        
        return Ok(response);
    }
    
    [HttpGet("{speciesId:guid}/breeds")]
    public async Task<ActionResult> GetBreed(
        [FromRoute] Guid speciesId,
        [FromQuery] GetBreedByIdSpeciesRequest request,
        [FromServices] GetBreedByIdSpeciesHandler handler,
        CancellationToken cancellationToken = default)
    {
        var queryResult = request.ToQuery(speciesId);
        
        var response = await handler.Handle(queryResult, cancellationToken);
        
        return Ok(response);
    }
}