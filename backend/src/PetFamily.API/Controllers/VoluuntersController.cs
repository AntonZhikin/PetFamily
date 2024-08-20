using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Voluunters.CreateVoluunters;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("[controller]")]

public class VoluuntersController : ControllerBase
{
    
    [HttpPost]
    public async Task Create(
        [FromServices] CreateVoluunterHandler handler,
        [FromServices] CreateVoluunterRequest request,
        CancellationToken cancellationToken = default)
    {
        //вызов сервис для создания волонтера(вызов бизнес логики)
        var result = await handler.HandleAsync(request, cancellationToken);
    }
}