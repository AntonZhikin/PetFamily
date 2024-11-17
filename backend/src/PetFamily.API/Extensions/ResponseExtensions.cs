using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Responce;
using PetFamily.Domain.Shared;

namespace PetFamily.API.Extensions;

public static class ResponseExtensions
{ 
    public static ActionResult ToResponse(this Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
        
        var responceError = new ResponceError(error.Code, error.Message, null);

        var envelope = Envelope.Error([responceError]);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }
}