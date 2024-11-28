using Microsoft.AspNetCore.Mvc;

namespace PetFamily.API.Controllers;

public class FileController : ApplicationController
{
    [HttpPost]

    public async Task<IActionResult> CreateFile([FromForm] IFormFile file)
    {
        return Ok(file);
    }
}
