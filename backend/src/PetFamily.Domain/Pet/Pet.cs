﻿namespace PetFamily.Domain.Pet;

public class Pet
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;
    
    public string Species { get; private set; } = null!;
    
    public string Description { get; private set; } = null!;
    
    public string Breed { get; private set; } = null!;

    public string Color { get; private set; } = null!;

    public string PetHealthInfo { get; private set; } = null!;

    public string Address { get; private set; } = null!;

    public float Weight { get; private set; } = 0!;

    public float Height { get; private set; } = 0!;

    public string PhoneNumber { get; private set; } = null!;
    
    public bool IsNeutered { get; private set; }
    
    public DateOnly DateOfBirth { get; private set; }
    
    public bool IsVaccine { get; private set; }

    public string Status { get; private set; } = null!;
    
    public enum Info
    {
        NeedsHelp,
        LookingForHome,
        FoundHome
    }
    
    public List<Requisite> Requisites { get; private set; } = [];
    
    public DateOnly DateCreate { get; private set; }
}