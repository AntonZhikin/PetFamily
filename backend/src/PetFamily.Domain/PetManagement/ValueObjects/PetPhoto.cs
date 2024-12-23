using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Domain.PetManagement.ValueObjects;

public record PetPhoto 
{
    private PetPhoto()
    {
        
    }
    
    public PetPhoto(string pathToStorage, bool isMain)
    {
        PathToStorage = pathToStorage;
        IsMain = isMain;
    }
    
    public string PathToStorage { get; }
    
   public bool IsMain { get; set; }
   
   public static Result<PetPhoto, ErrorList> Create(string path,
       bool isMain)
   {
       if (string.IsNullOrWhiteSpace(path))
           return Errors.General.ValueIsInvalid(nameof(Path)).ToErrorList();

       var newPhoto = new PetPhoto(path, isMain);

       return newPhoto;
   }
}