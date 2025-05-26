using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.VolunteerRequest.Application;

public interface IVolunteerRequestsRepository
{
    public Task Add(Domain.VolunteerRequest volunteerRequest, CancellationToken cancellationToken);
    
    public Task<Result<Domain.VolunteerRequest, Error>> GetById(
        Guid requestId, CancellationToken cancellationToken = default);
}