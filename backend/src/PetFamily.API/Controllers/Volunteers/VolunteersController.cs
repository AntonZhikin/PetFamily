using System.Reflection.Metadata;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Volunteers.Request;
using PetFamily.API.Extensions;
using PetFamily.API.Responce;
using PetFamily.Application.PetManagement.Commands.AddPet;
using PetFamily.Application.PetManagement.Commands.Create;
using PetFamily.Application.PetManagement.Commands.DeleteFilesToPet;
using PetFamily.Application.PetManagement.Commands.DeleteHard;
using PetFamily.Application.PetManagement.Commands.DeleteSoft;
using PetFamily.Application.PetManagement.Commands.MovePositionPet;
using PetFamily.Application.PetManagement.Commands.UpdateAssistanceDetail;
using PetFamily.Application.PetManagement.Commands.UpdateMainInfo;
using PetFamily.Application.PetManagement.Commands.UpdateSocialNetworks;
using PetFamily.Application.PetManagement.Commands.UploadFilesToPet;
using PetFamily.Application.PetManagement.Queries.GetVolunteersWithPagination;
using PetFamily.Application.Processors;

namespace PetFamily.API.Controllers.Volunteers;

public class VolunteersController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetVolunteersWithPaginationRequest request,
        [FromServices] GetVolunteersWithPaginationHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        
        var responce = await handler.Handle(query, cancellationToken);
        
        return Ok(responce);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromServices] UpdateMainInfoHandler handler,
        [FromBody] UpdateMainInfoRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<ActionResult> UpdateSocialNetwork(
        [FromRoute] Guid id,
        [FromServices] UpdateSocialNetworkHandler handler,
        [FromBody] UpdateSocialNetworkRequest request,
        [FromServices] IValidator<UpdateSocialNetworkCommand> validator,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/assistance-detail")]
    public async Task<ActionResult> UpdateAssistanceDetail(
        [FromRoute] Guid id,
        [FromServices] UpdateAssistanceDetailHandler handler,
        [FromBody] UpdateAssistanceDetailRequest request,
        [FromServices] IValidator<UpdateAssistanceDetailCommand> validator,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}/soft-delete")]
    public async Task<ActionResult> DeleteVolunteerSoft(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new DeleteVolunteerCommand(id);

        var result = await handler.Handle(request, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}/hard-delete")]
    public async Task<ActionResult> DeleteVolunteerHard(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerHardHandler handler,
        [FromServices] IValidator<DeleteVolunteerHardCommand> validator,
        CancellationToken cancellationToken)
    {
        var request = new DeleteVolunteerHardCommand(id);

        var result = await handler.Handle(request, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{id:guid}pet")]
    public async Task<ActionResult> AddPet(
        [FromRoute] Guid id,
        [FromBody] AddPetRequest request,
        [FromServices] AddPetHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{id:guid}pet/{petId:guid}/files")]
    public async Task<ActionResult> UploadFilesToPet(
        [FromRoute] Guid id, 
        [FromRoute] Guid petId,
        [FromForm] IFormFileCollection files,
        [FromServices] UploadFileToPetHandler handler,
        CancellationToken cancellationToken
        )
    {
        await using var fileProcessor = new FormFileProcessor();
        var fileDtos = fileProcessor.Process(files);
        
        var command = new UploadFileToPetCommand(id, petId, fileDtos);
        
        var result = await handler.Handle(command, cancellationToken);
        if(result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpPost("{id:guid}pet/{petId:guid}/deletefiles")]
    public async Task<ActionResult> DeleteFilesToPet(
        [FromRoute] Guid id, 
        [FromRoute] Guid petId,
        [FromForm] IFormFileCollection files,
        [FromServices] DeleteFilesToPetHandler handler,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteFilesToPetCommand(id, petId);
        
        var result = await handler.Handle(command, cancellationToken);
        if(result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpPost("{id:guid}pet/{petId:guid}/moveposition")]
    public async Task<ActionResult> MovePetPosition(
        [FromRoute] Guid id, 
        [FromRoute] Guid petId,
        [FromBody] MovePositionPetRequest request,
        [FromServices] MovePositionPetHandler handler,
        CancellationToken cancellationToken
    )
    {
        var result = await handler.Handle(request.ToCommand(id, petId), cancellationToken);
        
        if(result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}