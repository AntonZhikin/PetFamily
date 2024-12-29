using PetFamily.Core.DTOs;

namespace PetFamily.Species.Application;

public interface IReadDbContext
{
    
    IQueryable<SpeciesDto> Species { get; }
    IQueryable<BreedDto> Breed { get; }
    IQueryable<PetDto> Pets { get; }
}