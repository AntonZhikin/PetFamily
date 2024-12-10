using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using PetFamily.API.Extensions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.FIles.AddPet;
using PetFamily.Application.Volunteers.FIles.DeletePet;
using PetFamily.Application.Volunteers.FIles.GetPet;
using PetFamily.Infrastructure.Options;

namespace PetFamily.API.Controllers;

public class FileController : ApplicationController
{
    private readonly IMinioClient _minioClient;

    public FileController(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPet(
        IFormFile file, 
        [FromServices] AddPetFilesHandler filesHandler,
        CancellationToken cancellationToken)
    {
        await using var stream = file.OpenReadStream();

        var path = Guid.NewGuid().ToString();

        var fileContent = new FileContent(stream, path);
        
        //var result = await filesHandler.Handle(fileContent, "photos", cancellationToken);
        //if (result.IsFailure)
        //    return result.Error.ToResponse();
        
        //return Ok(result.Value);
        return null;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPet(
        Guid id,
        [FromServices] GetPetFilesHandler filesHandler,
        CancellationToken cancellationToken)
    {
        var objectName = id.ToString();
        
        var fileContent = new FileContent(null, objectName);
        
        var result = await filesHandler.Handle(fileContent, "photos", cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePet(
        Guid id,
        [FromServices] DeletePetFilesHandler filesHandler,
        CancellationToken cancellationToken)
    {
        var objectName = id.ToString();
        
        //var fileData = new FileData(null, objectName);
        
        //var result = await filesHandler.Handle(fileData, "photos", cancellationToken);
        //if (result.IsFailure)
        //    return result.Error.ToResponse();
        //
        //return Ok(result.Value);
        return null;

    }
}
