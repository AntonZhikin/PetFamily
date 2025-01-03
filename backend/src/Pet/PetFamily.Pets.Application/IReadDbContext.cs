using PetFamily.Core.DTOs;

namespace PetFamily.Pets.Application;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<PetDto> Pets { get; }
    
    IQueryable<SpeciesDto> Species { get; }
    IQueryable<BreedDto> Breed { get; }
}