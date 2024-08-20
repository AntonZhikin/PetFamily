using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Infrastructure.Repositories;

public class VoluuntersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VoluuntersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.AddAsync(volunteer);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }
    
    public async Task<Volunteer> GetById(VolunteerId volunteerId)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v =>v.Pets)
            .FirstOrDefaultAsync(v =>v.Id == volunteerId);

        if (volunteer is null)
        {
            throw new Exception("Voluunter can not made null");
        }

        return volunteer;
    }
}