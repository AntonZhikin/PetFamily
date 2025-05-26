using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Kernel;
using PetFamily.VolunteerRequest.Application;
using PetFamily.VolunteerRequest.Infrastructure.DbContext;

namespace PetFamily.VolunteerRequest.Infrastructure;

public class VolunteerRequestsRepository : IVolunteerRequestsRepository
{
    private readonly VolunteerRequestsWriteDbContext _dbContext;

    public VolunteerRequestsRepository(VolunteerRequestsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(
        Domain.VolunteerRequest volunteerRequest,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.VolunteerRequests.AddAsync(volunteerRequest, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Result<Domain.VolunteerRequest, Error>> GetById(
        Guid requestId,
        CancellationToken cancellationToken = default)
    {
        var request = await _dbContext.VolunteerRequests.
            SingleOrDefaultAsync(r => r.Id == requestId, cancellationToken);
        if (request is null)
            return Errors.General.NotFound(requestId);

        return request;
    }
}