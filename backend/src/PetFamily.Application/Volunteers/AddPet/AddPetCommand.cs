using PetFamily.Application.Volunteers.DTOs;
using PetFamily.Domain.Pet.PetValueObject;

namespace PetFamily.Application.Volunteers.AddPet;

public record AddPetCommand(
    Guid VolunteerId, 
    string Name, 
    Guid SpeciesId,
    Guid BreedId,
    string Description, 
    string Color, 
    string PetHealthInfo, 
    string City,
    string Street,
    float Weight, 
    float Height, 
    string PhoneNumber, 
    bool IsNeutered, 
    HelpStatus HelpStatus,
    DateTime DateOfBirth, 
    bool IsVaccine,
    DateTime DateCreate,
    RequisiteListDto RequisiteList);
    