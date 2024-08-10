namespace PetFamily.Domain.Pet;

public class Pet
{
    private Guid Id { get; set; }

    private string Name { get; set; } = null!;
    
    private string View { get; set; } = null!;
    
    private string Description { get; set; } = null!;
    
    private string Breed { get; set; } = null!;

    private string Color { get; set; } = null!;

    private string PetHealthInfo { get; set; } = null!;

    private string Address { get; set; } = null!;

    private float Weight { get; set; } = 0!;

    private float Height { get; set; } = 0!;

    private string PhoneNumber { get; set; } = null!;
    
    private bool isNeutered { get; set; }
    
    private DateOnly DateOfBirth { get; set; }
    
    private bool isVactine { get; set; }

    private enum Status
    {
        NeedsHelp,
        LookingForHome,
        FoundHome
    }

    private List<Requisite> Requisites { get; set; } = [];
    private DateOnly DateCreate { get; set; }
}
