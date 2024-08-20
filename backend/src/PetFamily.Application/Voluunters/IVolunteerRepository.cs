using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Application.Voluunters;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken);
}