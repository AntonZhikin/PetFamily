namespace PetFamily.Domain.Pet;

public class Pet
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
    public string King { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public string Breed { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string PetHealthInfo { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int Weight { get; set; } = 0!;

    public int Height { get; set; } = 0!;
    
    public decimal PhoneNumber { get; set; }
    
    public bool Neutered { get; set; }
    
    public DateOnly DateOfBirth { get; set; }
    
    public bool Vactine { get; set; }

    public string Status { get; set; } = null!;

    public List<Details> Detail { get; set; } = [];

    public DateOnly DateCreate { get; set; }
}
