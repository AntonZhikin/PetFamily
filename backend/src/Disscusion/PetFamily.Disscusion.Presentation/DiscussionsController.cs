using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Disscusion.Application.DiscussionManagement.Commands;
using PetFamily.Disscusion.Application.DiscussionManagement.Commands.Close;
using PetFamily.Disscusion.Application.DiscussionManagement.Commands.DeleteMessage;
using PetFamily.Disscusion.Application.DiscussionManagement.Commands.EditMessage;
using PetFamily.Disscusion.Application.DiscussionManagement.Commands.SendMessage;
using PetFamily.Disscusion.Application.DiscussionManagement.Queries;
using PetFamily.Disscusion.Presentation.Requests;
using PetFamily.Framework;
using PetFamily.Framework.Authorization;

namespace PetFamily.Disscusion.Presentation;

[Authorize]
public class DiscussionsController : ApplicationController
{
    [Permission(PermissionsConfig.Discussions.Create)]
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody] CreateRequest request,
        [FromServices] CreateHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(
            request.ToCommand(), cancellationToken);

        if (result.IsFailure) 
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    [HttpPut("close")]
    [Permission(PermissionsConfig.Discussions.Update)]
    public async Task<ActionResult> Close(
        [FromBody] CloseRequest request,
        [FromServices] CloseHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(
            request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [Permission(PermissionsConfig.Discussions.Update)]
    [HttpPut("Send")]
    public async Task<ActionResult> Message(
        [FromBody] SendMessageRequest request,
        [FromServices] SendMessageHandler handler,
        CancellationToken cancellationToken
    )
    {
        var result = await handler.Handle(
            request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [Permission(PermissionsConfig.Discussions.Update)]
    [HttpPut("edit-message")]
    public async Task<ActionResult<Guid>> EditMessage(
        [FromRoute] Guid userId,
        [FromRoute] Guid discussionId,
        [FromBody] EditMessageRequest request,
        [FromServices] EditMessageHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(
            request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [Permission(PermissionsConfig.Discussions.Update)]
    [HttpPut("delete-message")]
    public async Task<ActionResult<Guid>> DeleteMessage(
        [FromRoute] Guid userId,
        [FromRoute] Guid discussionId,
        [FromBody] DeleteMessageRequest request,
        [FromServices] DeleteMessageHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(
            request.ToCommand(userId, discussionId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [Permission(PermissionsConfig.Discussions.Read)]
    [HttpGet("{discussionId:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid discussionId,
        [FromServices] GetByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(
            new GetByIdQuery(discussionId),
            cancellationToken);

        return Ok(result.Value);
    }
}