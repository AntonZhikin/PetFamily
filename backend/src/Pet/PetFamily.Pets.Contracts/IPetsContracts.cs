using PetFamily.Core.DTOs;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.Pets.Contracts.Request;

namespace PetFamily.Pets.Contracts;

public interface IPetsContracts
{
    Task<PetDto?> AnyPetWithSpeciesId(AnyPetWithSpeciesIdRequest request, CancellationToken cancellationToken = default);
    
    Task<PetDto?> AnyPetWithBreedId(AnyPetWithBreedIdRequest request, CancellationToken cancellationToken = default);
}