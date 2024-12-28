using FluentValidation;
using PetFamily.Core.DTOs.ValueObject;
using PetFamily.Core.Validation;
using PetFamily.Kernel;

namespace PetFamily.Core.DTOs.Validator;

public class UploadFileDtoValidator : AbstractValidator<UploadFileDto>
{
    public UploadFileDtoValidator()
    {
        RuleFor(u => u.FileName).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.Content).Must(c => c.Length < 5000000);
    }
}