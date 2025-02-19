using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Domain.Accounts.ValueObjects;

public record AssistanceDetail
{
    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;
    
    public AssistanceDetail(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public static Result<AssistanceDetail, Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid(nameof(Name));
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsInvalid(nameof(Description));
        
        return new AssistanceDetail(name, description);
    }
}