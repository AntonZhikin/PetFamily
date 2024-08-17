namespace PetFamily.Domain.Pet;

public record PetList()
{
    public List<PetPhoto> PetPhotos { get; private set; } 
}