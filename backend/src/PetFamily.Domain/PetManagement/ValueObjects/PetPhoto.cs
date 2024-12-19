using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Domain.PetManagement.ValueObjects;

public record PetPhoto 
{
    public PetPhoto(PhotoPath pathToStorage)
    {
        PathToStorage = pathToStorage;
    }
    
    public PhotoPath PathToStorage { get; }

}