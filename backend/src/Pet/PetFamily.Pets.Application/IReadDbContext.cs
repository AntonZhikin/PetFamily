using PetFamily.Core.DTOs;
using PetFamily.Core.DTOs.Pets;
using PetFamily.Core.DTOs.Species;
using PetFamily.Core.DTOs.Volunteer;

namespace PetFamily.Pets.Application;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<PetDto> Pets { get; }
    
    IQueryable<SpeciesDto> Species { get; }
    IQueryable<BreedDto> Breed { get; }
}