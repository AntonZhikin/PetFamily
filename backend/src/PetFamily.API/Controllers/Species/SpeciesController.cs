using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Species.Request;
using PetFamily.API.Controllers.Volunteers.Request;
using PetFamily.API.Extensions;
using PetFamily.Application.Species.AddBreedToSpecies;
using PetFamily.Application.Species.Create;
using PetFamily.Application.Volunteers.AddPet;
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
}