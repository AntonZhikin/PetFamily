using FluentValidation;
using PetFamily.Core;
using PetFamily.Core.DTOs.Validator;
using PetFamily.Core.Validation;
using PetFamily.Kernel;

namespace PetFamily.Pets.Application.PetManagement.Commands.UploadFilesToPet;

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