using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.Species.Application.Species;
using PetFamily.Species.Infrastructure.DbContext;

namespace PetFamily.Species.Infrastructure.Repository;

 public class SpeciesRepository : ISpeciesRepository
{
    
    private readonly WriteDbContext _dbContext;

    public SpeciesRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(
        Domain.SpeciesManagement.AggregateRoot.Species species,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Species.AddAsync(species, cancellationToken);
        
        return species.Id.Value;
    }

    public Guid Save(Domain.SpeciesManagement.AggregateRoot.Species species)
    {
        _dbContext.Species.Attach(species);
        
        return species.Id.Value;
    }

    public async Task<Guid> Delete(Domain.SpeciesManagement.AggregateRoot.Species species)
    {
        _dbContext.Species
            .Remove(species);
        
        return species.Id.Value;
    }

    public async Task<Result<Domain.SpeciesManagement.AggregateRoot.Species, Error>> GetById(
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