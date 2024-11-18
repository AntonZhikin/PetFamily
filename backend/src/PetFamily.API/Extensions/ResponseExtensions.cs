using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Responce;
using PetFamily.Domain.Shared;
using ValidationResult = FluentValidation.Results.ValidationResult;

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
    
    public static ActionResult ToValidationErrorResponse(this ValidationResult result)
    {
        if (result.IsValid)
            throw new InvalidOperationException("Result can not be succeed");
        

        var validationErrors = result.Errors; 
            
        List<ResponceError> responceErrors = [];
        responceErrors.AddRange(from validationError in validationErrors 
            let errorMessage = validationError.ErrorMessage 
            let error = Error.DeSerialize(errorMessage) 
                select new ResponceError(error.Code, error.Message, validationError.PropertyName));

        var envelope = Envelope.Error(responceErrors);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}