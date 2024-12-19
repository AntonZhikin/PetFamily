using PetFamily.Application.DTOs.ValueObject;

namespace PetFamily.Application.DTOs;

public class PetDto
{
    public Guid Id { get; init; }
     
    public Guid VolunteerId { get; init; }
    
    /*public int Position { get; private set; }
    
    public string Name { get; init; } = string.Empty;
    
    public string Description { get; init; } = string.Empty;
     
    public string Color { get; init; } = string.Empty;

    public string PetHealthInfo { get; init; } = string.Empty;

    public string Address { get; init; } = string.Empty;

    public float Weight { get; init; }

    public float Height { get; init; }

    public string PhoneNumber { get; init; } = string.Empty;

    public bool IsNeutered { get; private set; } = false;
    
    public DateTime DateOfBirth { get; init; }
    
    public bool IsVaccine { get; init; } = false;

    public string HelpStatus { get; init; }
    
    public DateTime DateCreate { get; init; }*/
     
    public Guid SpeciesId { get; init; }
     
    public Guid BreedId { get; init; }
     
    public PetPhotoDto[] Photos { get; init; } = [];

}