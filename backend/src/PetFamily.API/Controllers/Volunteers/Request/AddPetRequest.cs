using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.DTOs;
using PetFamily.Domain.PetManagement.ValueObjects;

namespace PetFamily.API.Controllers.Volunteers.Request;

public record AddPetRequest(
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
    RequisiteListDto Requisites)
{
    public AddPetCommand ToCommand(Guid volunteerId) => new(
        volunteerId,
        Name,
        SpeciesId,
        BreedId,
        Description,
        Color,
        PetHealthInfo,
        City,
        Street,
        Weight,
        Height,
        PhoneNumber,
        IsNeutered,
        HelpStatus,
        DateOfBirth,
        IsVaccine,
        DateCreate,
        Requisites
    );
}

