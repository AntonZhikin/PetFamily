using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet;

public class PetPhoto
{
    private PetPhoto()
    {
        
    }
    public string Path { get; }
    
    public bool IsMain { get; }

    public static string Create(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new Exception(); //просто заглушка пока не дошел до 5/1
        }

        return "ok";
    }
}