using PetFamily.Core.DTOs;
using PetFamily.Core.DTOs.Pets;
using PetFamily.Core.DTOs.Species;

namespace PetFamily.Species.Application;

public interface IReadDbContext
{
    IQueryable<SpeciesDto> Species { get; }
    IQueryable<BreedDto> Breed { get; }
    IQueryable<PetDto> Pets { get; }
}