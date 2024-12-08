using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Speciess.SpeciesID;

namespace PetFamily.Application.Species;

public interface ISpeciesRepository
{
    Task<Guid> Add(Domain.SpeciesManagement.AggregateRoot.Species species, CancellationToken cancellationToken = default);
    Guid Save(Domain.SpeciesManagement.AggregateRoot.Species species);
    Task<Guid> Delete(Domain.SpeciesManagement.AggregateRoot.Species species, CancellationToken cancellationToken = default);
    
    Task<Result<Domain.SpeciesManagement.AggregateRoot.Species, Error>> GetById(SpeciesId speciesId, CancellationToken cancellationToken = default);
}