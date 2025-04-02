namespace PetFamily.Core.DTOs.Species;

public class BreedDto
{
    public Guid Id { get; set; }
    
    public Guid SpeciesId { get; set; }
    
    public string Name { get; set; }
}