using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Responce;

namespace PetFamily.API.Controllers;


[ApiController]
[Route("[controller]")]
public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);
        
        return base.Ok(envelope);
    }
}