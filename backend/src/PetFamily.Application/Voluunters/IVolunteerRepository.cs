using PetFamily.Domain.Volunteer;

namespace PetFamily.Application.Voluunters;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);

    Task<Volunteer> GetById(VolunteerId volunteerId, CancellationToken cancellationToken);
}