using CSharpFunctionalExtensions;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.Pets.Domain.AggregateRoot;

namespace PetFamily.Pets.Application.PetManagement;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer);

    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);
    
    Task<Guid> DeleteHard(Volunteer volunteer);
}