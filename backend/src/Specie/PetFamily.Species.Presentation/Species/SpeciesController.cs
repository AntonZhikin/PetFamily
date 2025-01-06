using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Core;
using PetFamily.Core.Extensions;
using PetFamily.Framework;
using PetFamily.Species.Application.Species.Commands.AddBreedToSpecies;
using PetFamily.Species.Application.Species.Commands.Create;
using PetFamily.Species.Application.Species.Commands.Delete;
using PetFamily.Species.Application.Species.Commands.DeleteBreed;
using PetFamily.Species.Application.Species.Queries.GetBreedByIdSpecies;
using PetFamily.Species.Application.Species.Queries.GetSpeciesWithPagination;
using PetFamily.Species.Presentation.Species.Request;

namespace PetFamily.Species.Presentation.Species;

public class SpeciesController : ApplicationController
{
    [Authorize]
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
    
    [Authorize]
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
    
    [Authorize]
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
    
    [Authorize]
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