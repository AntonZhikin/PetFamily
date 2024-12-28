namespace PetFamily.Pets.Domain.ValueObjects;

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