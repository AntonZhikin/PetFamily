using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Voluunters.CreateVoluunters;
using PetFamily.Domain.Shared;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("[controller]")]

public class VolunteersController : ControllerBase
{
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        //вызов сервис для создания волонтера(вызов бизнес логики)
        var result = await handler.Handle(request, cancellationToken);
        
        if (result.IsFailure)
            return result.ToResponse();

        return result.ToResponse();
    }
}