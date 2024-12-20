using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Species;
using PetFamily.Domain.Shared.Error;
using PetFamily.Domain.SpeciesManagement.AggregateRoot;
using PetFamily.Domain.SpeciesManagement.Ids;
using PetFamily.Infrastructure.DbContext;

namespace PetFamily.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WriteDbContext _dbContext;

    public SpeciesRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(
        Species species,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Species.AddAsync(species, cancellationToken);
        
        return species.Id.Value;
    }

    public Guid Save(Species species)
    {
        _dbContext.Species.Attach(species);
        
        return species.Id.Value;
    }

    public async Task<Guid> Delete(Species species)
    {
        _dbContext.Species
            .Remove(species);
        
        return species.Id.Value;
    }

    public async Task<Result<Species, Error>> GetById(
        SpeciesId speciesId, 
        CancellationToken cancellationToken = default)
    {
        var species = await _dbContext.Species
            .Include(v => v.Breeds)
            .FirstOrDefaultAsync(v => v.Id == speciesId, cancellationToken);

        if (species is null)
        {
            return Errors.General.NotFound();

        }
        return species;
    }
    
}