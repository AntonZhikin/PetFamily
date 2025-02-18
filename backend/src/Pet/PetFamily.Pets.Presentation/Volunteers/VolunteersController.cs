using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Framework;
using PetFamily.Kernel.ValueObject;
using PetFamily.Pets.Application.PetManagement.Commands.AddPet;
using PetFamily.Pets.Application.PetManagement.Commands.Create;
using PetFamily.Pets.Application.PetManagement.Commands.DeleteFilesToPet;
using PetFamily.Pets.Application.PetManagement.Commands.DeleteHard;
using PetFamily.Pets.Application.PetManagement.Commands.DeleteHardPet;
using PetFamily.Pets.Application.PetManagement.Commands.DeleteSoft;
using PetFamily.Pets.Application.PetManagement.Commands.DeleteSoftPet;
using PetFamily.Pets.Application.PetManagement.Commands.MovePositionPet;
using PetFamily.Pets.Application.PetManagement.Commands.UpdateMainInfo;
using PetFamily.Pets.Application.PetManagement.Commands.UpdatePet;
using PetFamily.Pets.Application.PetManagement.Commands.UpdatePetMainPhoto;
using PetFamily.Pets.Application.PetManagement.Commands.UpdatePetStatus;
using PetFamily.Pets.Application.PetManagement.Commands.UploadFilesToPet;
using PetFamily.Pets.Application.PetManagement.Queries.GetPetById;
using PetFamily.Pets.Application.PetManagement.Queries.GetPetsWithPagination;
using PetFamily.Pets.Application.PetManagement.Queries.GetVolunteerByIdQuery;
using PetFamily.Pets.Application.PetManagement.Queries.GetVolunteersWithPagination;
using PetFamily.Pets.Controllers.Volunteers.Request;

namespace PetFamily.Pets.Controllers.Volunteers;

public class VolunteersController : ApplicationController
{
    [HttpGet("volunteer-by-id")]
    public async Task<ActionResult> GetById(
        [FromQuery] GetVolunteerByIdRequest request,
        [FromServices] GetVolunteerByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        
        var response = await handler.Handle(query, cancellationToken);
        
        return Ok(response);
    }

    
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetVolunteersWithPaginationRequest request,
        [FromServices] GetVolunteersWithPaginationHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        
        var response = await handler.Handle(query, cancellationToken);
        
        return Ok(response);
    }
    
    [Authorize]
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
    
    [Authorize]
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
    
    /*[Authorize]
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
    }*/
    
    /*[Authorize]
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
    }*/
    
    [Authorize]
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
        
    [Authorize]
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
    
    [Authorize]
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
    
    [Authorize]
    [HttpPost("{id:guid}pet/{petId:guid}/files")]
    public async Task<ActionResult> UploadFilesToPet(
        [FromRoute] Guid id, 
        [FromRoute] Guid petId,
        [FromForm] IFormFileCollection files,
        [FromServices] UploadFileToPetHandler handler,
        CancellationToken cancellationToken
        )
    {
        // await using var fileProcessor = new FormFileProcessor();
        // var fileDtos = fileProcessor.Process(files);
        //
        // var command = new UploadFileToPetCommand(id, petId, fileDtos);
        //
        // var result = await handler.Handle(command, cancellationToken);
        // if(result.IsFailure)
        //     return result.Error.ToResponse();
        //
        //return Ok(result.Value);
        return Ok();
    }
    
    [Authorize]
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
    
    [Authorize]
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
        
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [Authorize]
    [HttpPut("{id:guid}pet")]
    public async Task<ActionResult> UpdatePet(
        [FromRoute] Guid id,
        [FromServices] UpdatePetHandler handler,
        [FromBody] UpdatePetRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [Authorize]
    [HttpPut("{volunteerId:guid}pet{petId:guid}/status")]
    public async Task<ActionResult> UpdatePetStatus(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] UpdatePetStatusHandler handler,
        [FromBody] UpdatePetStatusRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdatePetStatusCommand(volunteerId, petId, request.NewStatus);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [Authorize]
    [HttpDelete("{volunteerId:guid}pet{petId:guid}")]
    public async Task<ActionResult> DeleteSoftPet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] DeleteSoftPetHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteSoftPetCommand(volunteerId, petId);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [Authorize]
    [HttpDelete("{volunteerId:guid}pet{petId:guid}/hard")]
    public async Task<ActionResult> DeleteHardPet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] DeleteHardPetHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteHardPetCommand(volunteerId, petId);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [Authorize]
    [HttpPut("volunteer{volunteerId:guid}pet{petId:guid}/mainPhoto")]
    public async Task<ActionResult> UpdatePetMainPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetMainPhotoRequest request,
        [FromServices] UpdatePetMainPhotoHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new UpdatePetMainPhotoCommand(PhotoPath.Create(request.PathToStorage).Value, volunteerId, petId);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value); 
    }
    
    [HttpGet("PetById")]
    public async Task<ActionResult> GetPetById(
        [FromQuery] GetPetByIdRequest request,
        [FromServices] GetPetByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        
        var response = await handler.Handle(query, cancellationToken);
        
        return Ok(response);
    }
    
    [HttpGet("Pets")]
    public async Task<ActionResult> GetPets(
        [FromQuery] GetPetsWithPaginationRequest request,
        [FromServices] GetPetsWithPaginationHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        
        var response = await handler.Handle(query, cancellationToken);
        
        return Ok(response);
    }
}