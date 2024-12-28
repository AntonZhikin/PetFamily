using PetFamily.Core.DTOs.ValueObject;
using PetFamily.Pets.Application.PetManagement.Commands.UpdatePet;
using PetFamily.Pets.Domain.ValueObjects;

namespace PetFamily.Pets.Controllers.Volunteers.Request;

public record UpdatePetRequest(
    Guid PetId,
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
    public UpdatePetCommand ToCommand(Guid volunteerId)
        => new UpdatePetCommand(
            volunteerId,
            PetId,
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
            Requisites);
}
