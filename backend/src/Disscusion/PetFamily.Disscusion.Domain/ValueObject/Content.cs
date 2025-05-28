using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Domain.ValueObject;

public class Content 
{
    private Content(string value)
    {
        Value = value;
    }
    public string Value { get; } = default!;

    public static Result<Content, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid(nameof(Content));
        
        return new Content(value);
    }
}