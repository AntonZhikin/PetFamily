using PetFamily.Core.DTOs;
using PetFamily.Core.DTOs.Species;
using PetFamily.Species.Contracts.Request;

namespace PetFamily.Species.Contracts;

public interface ISpecieContract
{
    Task<SpeciesDto?> GetSpeciesById(GetSpecieByIdRequest request, CancellationToken cancellationToken);

    Task<BreedDto?> GetBreedById(GetBreedByIdRequest request, CancellationToken cancellationToken);
}