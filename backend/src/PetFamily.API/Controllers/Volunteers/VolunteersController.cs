using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Volunteers.Request;
using PetFamily.API.Extensions;
using PetFamily.Application.Processors;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.DeleteFilesToPet;
using PetFamily.Application.Volunteers.DeleteHard;
using PetFamily.Application.Volunteers.DeleteSoft;
using PetFamily.Application.Volunteers.UpdateAssistanceDetail;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateSocialNetworks;
using PetFamily.Application.Volunteers.UploadFilesToPet;

namespace PetFamily.API.Controllers.Volunteers;

public class VolunteersController : ApplicationController
{
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
}