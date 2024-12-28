using CSharpFunctionalExtensions;
using PetFamily.Core;
using PetFamily.Kernel;

namespace PetFamily.Pets.Domain.ValueObjects;

public record SocialNetwork 
{
    public string Name { get; } = null!;
    
    public string Path { get; } = null!;
    

    public SocialNetwork(string path, string name)
    {
        Name = name;
        Path = path;
    }
    
    public static Result<SocialNetwork, Error> Create(string name, string path)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid("name");

        if (string.IsNullOrWhiteSpace(path))
            return Errors.General.ValueIsInvalid("path");
        
        return new SocialNetwork(name, path);
    }
}