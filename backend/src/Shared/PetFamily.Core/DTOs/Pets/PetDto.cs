using PetFamily.Core.DTOs.ValueObject;

namespace PetFamily.Core.DTOs.Pets;

public class PetDto
{
    public Guid Id { get; init; }
    public Guid VolunteerId { get; init; }
    public int Position { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
    public AddressDto Address { get; init; }
    public float Weight { get; init; }
    public float Height { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
    public bool IsNeutered { get; init; } 
    
    public DateTime DateOfBirth { get; init; }
    public bool IsVaccine { get; init; } 
    
    public HelpStatus HelpStatus { get; init; }
    
    public DateTime DateCreate { get; init; }
    public SpeciesBreedDto SpeciesBreedDto { get; init; }
    public string AvatarUrl { get; set; } = string.Empty;
    public MediaFileDto? Avatar { get; init; }
    public IReadOnlyList<string> PhotosUrls { get; set; } = [];
    public IReadOnlyList<MediaFileDto> Photos { get; set; } = [];
}