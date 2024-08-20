using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Voluunters;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VolunteersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.AddAsync(volunteer);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }
    
    public async Task<Result<Volunteer, Error>> GetById(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v =>v.Pets)
            .FirstOrDefaultAsync(v =>v.Id == volunteerId);

        if (volunteer is null)
        {
            return Errors.General.NotFound(volunteerId);
        }

        return volunteer;
    }
}