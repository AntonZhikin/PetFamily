using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs.ValueObject;
using PetFamily.Pets.Domain.ValueObjects;

namespace PetFamily.Pets.Application.PetManagement.Commands.AddPet;

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
    RequisiteListDto RequisiteList) : ICommand;
    