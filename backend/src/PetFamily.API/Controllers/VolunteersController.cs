using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Volunteers.CreateVolunteers;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("[controller]")]

public class VolunteersController : ControllerBase
{
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerCommand command,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        //вызов сервис для создания волонтера(вызов бизнес логики)
        var result = await command.Handle(request, cancellationToken);
         
        if (result.IsFailure)
            return result.ToResponse();

        return result.ToResponse();
    }
    
    [HttpGet]
    public IActionResult Update()
    {
        return Ok();
    }
    
    
}