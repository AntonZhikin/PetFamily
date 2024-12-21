using CSharpFunctionalExtensions;
using PetFamily.Domain.PetManagement.AggregateRoot;
using PetFamily.Domain.PetManagement.Ids;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.PetManagement;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);
    
    Task<Guid> DeleteHard(Volunteer volunteer);
    
    
}