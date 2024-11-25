namespace PetFamily.Domain.Pet.PetLists;

public record PetPhotoList
{
    public IReadOnlyList<PetPhoto> PetPhotos { get; }
    
    private PetPhotoList() { }
    
    public PetPhotoList(IEnumerable<PetPhoto> petPhotos)
    {
        PetPhotos = petPhotos.ToList();
    }
}