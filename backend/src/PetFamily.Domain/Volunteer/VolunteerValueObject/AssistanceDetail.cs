using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteer;

public record AssistanceDetail
{
    public string Name { get; } = null!;

    public string Description { get; } = null!;
    
    private AssistanceDetail(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public static Result<AssistanceDetail, Error> Create(string name, string description)
    {
        var newAssistanceDetail = new AssistanceDetail(name, description);
        
        return newAssistanceDetail;
    }
}