using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.API.Responce;
using PetFamily.Application.Volunteers.CreateVolunteers;
using PetFamily.API.Extensions;
using PetFamily.Domain.Shared;

namespace PetFamily.API.Controllers;

public class VolunteersController : ApplicationController
{
    
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromServices] IValidator<CreateVolunteerRequest> validator,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid == false)
        {
            var validationErrors = validationResult.Errors;
            
            List<ResponceError> responceErrors = [];
            responceErrors.AddRange(from validationError in validationErrors 
                let errorMessage = validationError.ErrorMessage 
                let error = Error.DeSerialize(errorMessage) 
                select new ResponceError(error.Code, error.Message, validationError.PropertyName));

            var envelope = Envelope.Error(responceErrors);
            
            return BadRequest(envelope);
        }
        
        //вызов сервис для создания волонтера(вызов бизнес логики)
        var result = await handler.Handle(request, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return CreatedAtAction("", result.Value);
    }
}