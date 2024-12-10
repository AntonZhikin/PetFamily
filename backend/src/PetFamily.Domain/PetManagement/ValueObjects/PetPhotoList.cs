using PetFamily.Domain.Pet;

namespace PetFamily.Domain.PetManagement.ValueObjects;

public record PetPhotoList
{
    public IReadOnlyList<PetPhoto> PetPhotos { get; private set; }

    public void DeleteAllPhotos() => PetPhotos = [];
    
    private PetPhotoList() { }
    
    public PetPhotoList(IEnumerable<PetPhoto> petPhotos)
    {
        PetPhotos = petPhotos.ToList();
    }
}