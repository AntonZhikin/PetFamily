using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;

namespace PetFamily.Kernel.ValueObject;

public record PhotoPath
{
    [JsonConstructor]
    private PhotoPath(string path)
    {
        Path = path;
    }
    
    public string Path { get; }
    
    public static Result<PhotoPath, Error> Create(Guid path, string extension)
    {
        var fullPath = path + extension;
        
        return new PhotoPath(fullPath);  
    }
    public static Result<PhotoPath, Error> Create(string fullPath)
    {
        return new PhotoPath(fullPath);  
    }
} 