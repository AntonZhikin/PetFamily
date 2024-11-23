﻿using PetFamily.Domain.Pet.PetID;
using PetFamily.Domain.Pet.PetLists;
using PetFamily.Domain.Pet.PetValueObject;
using PetFamily.Domain.Shared;
namespace PetFamily.Domain.Pet;

public sealed class Pet : Entity<PetId>
{
    //ef core
    private Pet(PetId id) : base(id)
    {
    }

    private Pet(PetId petId, Name name) : base(petId)
    {
        Name = name;
    }
    
    public Name Name { get; }
    
    public Description Description { get; }

    public Color Color { get; }

    public PetHealthInfo PetHealthInfo { get; }

    public Address Address { get; }

    public Weight Weight { get; }

    public Height Height { get; }

    public PhoneNumber PhoneNumber { get; }

    public IsNautered IsNeutered { get; }
    
    public DateOnly DateOfBirth { get; }
    
    public bool IsVaccine { get; }

    public HelpStatus HelpStatus { get;}
    
    public RequisiteList Requisites { get;}
    public DateOnly DateCreate { get; }

    public PetPhotoList Photos { get;}
}