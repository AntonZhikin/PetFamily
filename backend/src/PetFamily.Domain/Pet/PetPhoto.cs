using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet;

public sealed class PetPhoto : Entity<PetPhotoId>
{
    //ef core
    private PetPhoto(PetPhotoId id) : base(id)
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