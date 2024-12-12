using FluentValidation.Results;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.Extensions;

public static class ValidationExtensions
{
    public static ErrorList ToErrorList(this ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;

        var errors = from validationError in validationErrors
            let errorMessage = validationError.ErrorMessage
            let error = Error.DeSerialize(errorMessage)
            select Error.Validation(error.Code, error.Message, validationError.PropertyName);

        return errors.ToList();
    }
}