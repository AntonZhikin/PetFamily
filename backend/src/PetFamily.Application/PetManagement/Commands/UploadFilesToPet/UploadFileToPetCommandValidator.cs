using FluentValidation;
using PetFamily.Application.DTOs.Validator;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.PetManagement.Commands.UploadFilesToPet;

public class UploadFileToPetCommandValidator : AbstractValidator<UploadFileToPetCommand>
{
    public UploadFileToPetCommandValidator()
    {
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.Files).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(u => u.Files).SetValidator(new UploadFileDtoValidator());
    }
}